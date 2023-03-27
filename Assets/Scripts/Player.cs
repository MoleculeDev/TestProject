using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("GameObjects")]
    Camera mainCamera;
    public Animator animator;
    public GameObject mesh;
    public GameObject simpleWalkParticle;
    public GameObject pointer;
    public List<GameObject> itemList;

    [Header("Transorms and Positions")]
    public List<Transform> itemHandleTransfroms;
    public Transform moneyTakingPosition;

    public NavMeshAgent navMeshAgent;
    
    [Header("Bools")]
    public bool control;
    public bool taking;
    public bool handsBusy;
    public bool buying;
    public bool puting;
    public bool moneyTaking;
    public bool itemTransforming;
    public bool coroutineIsActive;
    bool onZone;

    [Header("UI")]
    public GameObject canvas;

    [Header("Scripts")]
    GamePlayController gamePlayController;
    public InventoryHandler inventory;

    [Header("Floats & Ints")]
    public float maxHealth;
    public int handMaxContains;
    public float speed;
    public float rotationSpeed;
    float pointerHideDistance;

    //Control side
    [HideInInspector] public Rigidbody rb;
    public FloatingJoystick floatingJoystick;
    [HideInInspector] public Transform targetPosition;

    Quaternion pointerRotation;

    void Start()
    {
        pointerHideDistance = 10;
        control = true;
        rb = gameObject.GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        gamePlayController = GameObject.FindGameObjectWithTag("GPC").GetComponent<GamePlayController>();
        SetProperties();
    }

    void SetProperties()
    {
        handMaxContains = gamePlayController.gameData.playerMaxContains;
        maxHealth = gamePlayController.gameData.playerMaxHealth;
        speed = gamePlayController.gameData.playerSpeed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ItemGenerator") && onZone && itemList.Count < handMaxContains) 
        {
            ItemGenerator itemGenerator = other.gameObject.GetComponentInParent<ItemGenerator>();
            StartCoroutine(MoveToHand(itemList, itemGenerator.itemsList, itemHandleTransfroms, "Item"));
        }

        if (other.CompareTag("ItemReciever") && onZone && itemList.Count > 0)
        {
            ItemGenerator itemGenerator = other.gameObject.GetComponentInParent<ItemGenerator>();

            if (itemGenerator.itemsList.Count < itemGenerator.itemTransforms.Count) 
            {
                StartCoroutine(PutFromHand(itemGenerator.itemsList, itemList, itemGenerator.itemTransforms, "Item"));
            } 
        }

        if (other.CompareTag("MoneyHolder")) 
        {
            if (!moneyTaking && other.gameObject.GetComponentInParent<ItemGenerator>().moneyList.Count > 0)
            {
                StartCoroutine(GetMoney(other.gameObject.GetComponentInParent<ItemGenerator>().moneyList, other.gameObject));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemGenerator")) onZone = true;
        if (other.CompareTag("ItemReciever")) onZone = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ItemGenerator"))
        {
            onZone = false;
        }
    }


    IEnumerator PutToTrash(Transform trashPos, GameObject trash, List<GameObject> objectsList)
    {
        puting = true;
        GameObject currentObject;

        while ((objectsList.Count > 0) && onZone)
        {
            float time = 0;

            currentObject = objectsList[objectsList.Count - 1];
            trash.GetComponent<Animator>().SetTrigger("Open");
            

            currentObject.transform.SetParent(trashPos, true);
            Vector3 destination = new Vector3(0, 0, 0);
            Vector3 offset = destination + new Vector3(0, 6, 0);
            Vector3 startPosition = currentObject.transform.localPosition;

            Quaternion newRotation = Quaternion.Euler(-90, 0, 0);
            Quaternion startRotation = currentObject.transform.localRotation;
            gamePlayController.soundManager.ItemTake(objectsList.Count - 1);

            while (time < 0.2f)
            {
                currentObject.transform.localPosition = Vector3.Lerp(startPosition, offset, time / 0.2f);
                currentObject.transform.localRotation = Quaternion.Lerp(startRotation, newRotation, time / 0.2f);
                time += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            currentObject.transform.localPosition = offset;
            currentObject.transform.localRotation = newRotation;

            time = 0;

            while (time < 0.1f)
            {
                currentObject.transform.localPosition = Vector3.Lerp(offset, destination, time / 0.1f);
                time += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            trash.GetComponent<Animator>().SetTrigger("Recycle");

            int moneyCount = Random.Range(2, 4);


            objectsList.Remove(currentObject);
            Destroy(currentObject);

            objectsList = objectsList.Distinct().ToList();

            yield return new WaitForFixedUpdate();
        }

        puting = false;

        if (objectsList.Count == 0)
        {
            puting = false;
            handsBusy = false;
        }

    }
    IEnumerator MoveToHand(List<GameObject> listTo, List<GameObject> listFrom, List<Transform> positionList, string type)
    {
        #region taking
        //if (!handHolder.gameObject.activeInHierarchy) { handHolder.gameObject.SetActive(true); handHolder.gameObject.GetComponent<Animator>().SetTrigger("Enable"); }
        if (listFrom.Count > 0 && onZone && !taking) 
        {
            float time = 0;
            taking = true;
            GameObject gameObject = listFrom[listFrom.Count - 1];
            gameObject.transform.SetParent(positionList[listTo.Count], true);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if(type == "Battery")gameObject.GetComponent<Animator>().SetTrigger("Taking");
            Vector3 startPosition = gameObject.transform.localPosition;
            Vector3 offSetPosition = startPosition + new Vector3(0,Random.Range(2,5), 0);
            gamePlayController.soundManager.ItemTake(listTo.Count);

            Vector3 destination = new Vector3(0, 0, 0);
            //Vector3 localScale = new Vector3(0.4f, 0.4f, 0.4f);
            //gameObject.transform.localScale = localScale;

            while (time <= 0.1f)
            {
                gameObject.transform.localPosition = Vector3.Lerp(startPosition, offSetPosition, time / 0.1f);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0;

            while (time <= 0.1f)
            {
                gameObject.transform.localPosition = Vector3.Lerp(offSetPosition, destination, time / 0.1f);
                time += Time.deltaTime;
                yield return null;
            }

            taking = false;
            gameObject.transform.localPosition = destination;
            listTo.Add(gameObject);
            listFrom.Remove(gameObject);
            
        }
        
        #endregion

    }
    IEnumerator PutFromHand(List<GameObject> listTo, List<GameObject> listFrom, List<Transform> positionList, string type)
    {
        #region taking
        if (listFrom.Count > 0 && onZone && !taking)
        {
            float time = 0;
            taking = true;
            GameObject gameObject = listFrom[listFrom.Count - 1];
            gameObject.transform.SetParent(positionList[listTo.Count], true);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            
            Vector3 startPosition = gameObject.transform.localPosition;
            Vector3 offSetPosition = startPosition + new Vector3(0, Random.Range(2, 5), 0);
            gamePlayController.soundManager.ItemTake(listTo.Count);

            Vector3 destination = new Vector3(0, 0, 0);

            while (time <= 0.1f)
            {
                gameObject.transform.localPosition = Vector3.Lerp(startPosition, offSetPosition, time / 0.1f);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0;

            while (time <= 0.1f)
            {
                gameObject.transform.localPosition = Vector3.Lerp(offSetPosition, destination, time / 0.1f);
                time += Time.deltaTime;
                yield return null;
            }

            taking = false;
            gameObject.transform.localPosition = destination;
            listTo.Add(gameObject);
            listFrom.Remove(gameObject);

        }

        #endregion

    }
    IEnumerator GetMoney(List<GameObject> moneyList, GameObject referenceObject) 
    {
        moneyTaking = true;

        //for (int i = 0; i < moneyList.Count; i++)
        //{
        //    StartCoroutine(TransformingMoney(moneyList[i], moneyList));
        //}

        gamePlayController.soundManager.MoneyBill();

        while (moneyList.Count > 0) 
        {
            StartCoroutine(TransformingMoney(moneyList[moneyList.Count - 1], moneyList));

            yield return new WaitUntil(() => !itemTransforming);
        }

        float time = 0;
        int startMoney = gamePlayController.gameData.money;
        int endMoney = gamePlayController.gameData.money + referenceObject.GetComponentInParent<ItemGenerator>().recievedMoney;
        int currentMoney;
        //gamePlayController.moneyText_Parent.GetComponent<Animator>().SetTrigger("Scale");
        gamePlayController.soundManager.MoneyBill();

        while (time < 1)
        {
            currentMoney = (int)Mathf.Lerp(startMoney, endMoney, time / 1);
            gamePlayController.moneyText.text = currentMoney.ToString();
            time += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        gamePlayController.gameData.money += referenceObject.GetComponentInParent<ItemGenerator>().recievedMoney;
        referenceObject.GetComponentInParent<ItemGenerator>().recievedMoney = 0;
        moneyTaking = false;
    }
    public IEnumerator TransformingMoney(GameObject gameObject, List<GameObject> moneyList)
    {
        //yield return new WaitForSeconds(0.5f);
        itemTransforming = true;

        Vector3 startPosition = gameObject.transform.position;
        Vector3 destination = moneyTakingPosition.position;
        Vector3 offsetPosition = destination + new Vector3(0, 2, 0);

        #region
        float time = 0;
        while (time < 0.03f)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, offsetPosition, time / 0.03f);
            time += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        time = 0;

        destination = moneyTakingPosition.position;
        
        //offsetPosition = destination + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));

        while (time < 0.03f)
        {
            gameObject.transform.position = Vector3.Lerp(offsetPosition, destination, time / 0.03f);
            time += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        moneyList.Remove(gameObject);
        Destroy(gameObject);
        itemTransforming = false;
        #endregion
    }

    IEnumerator AddToInventory() 
    {
        yield return null;
    }

    void FixedUpdate()
    {
        UpdateMoveJoystick();
        //SetPointer();
    }

    private void LateUpdate()
    {
        //canvas.gameObject.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    /// <summary>
    /// Control, moving gameobject
    /// </summary>
    void UpdateMoveJoystick()
    {
        if (control)
        {
            float hoz = floatingJoystick.Horizontal;
            float ver = floatingJoystick.Vertical;

            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward = cameraForward.normalized;
            cameraRight = cameraRight.normalized;

            Vector3 forwardRelativeVertical = ver * cameraForward;
            Vector3 rightRelativeHorizontal = hoz * cameraRight;

            Vector3 relativeMovement = forwardRelativeVertical + rightRelativeHorizontal;

            navMeshAgent.velocity = relativeMovement * speed;


            if ((hoz > 0.1f || hoz < -0.1f) || (ver > 0.1f || ver < -0.1f))
            {
                if (itemList.Count > 0)
                {
                    animator.ResetTrigger("Walk");
                }
                else
                {
                    animator.SetTrigger("Walk");
                    animator.ResetTrigger("Idle");
                }

                if ((hoz > 0.7f || hoz < -0.7f) || (ver > 0.7f || ver < -0.7f))
                {
                    animator.SetTrigger("Run");
                    animator.ResetTrigger("Idle");
                    animator.ResetTrigger("Walk");

                    #region with items
                    //if (itemList.Count > 0)
                    //{
                    //    animator.ResetTrigger("Walk");
                    //    animator.SetTrigger("Run");
                    //}
                    //else
                    //{
                    //    animator.SetTrigger("Run");
                    //    animator.ResetTrigger("Idle");
                    //    animator.ResetTrigger("Walk");
                    //}
                    #endregion
                }

                navMeshAgent.transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity);
            }
            else if (hoz == 0 && ver == 0)
            {
                animator.ResetTrigger("Run");
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle");
                //animator.ResetTrigger("Run_Tray");

                #region with items
                //if (itemList.Count > 0) animator.SetTrigger("Idle_Tray");
                //else animator.SetTrigger("Idle");
                #endregion 
            }
        }
        
    }

    
    /// <summary>
    /// FootstepSound player
    /// </summary>
    public void FootStepSound() 
    {
        gamePlayController.soundManager.FootStep();
    }

    #region Pointer
    //Target position pointer
    void SetPointer()
    {
        if (gamePlayController.gameData.tutorialIsActive && targetPosition != null) 
        {
            Vector3 distance = pointer.transform.position - targetPosition.position;

            if(distance.magnitude < pointerHideDistance) 
            {
                pointer.SetActive(false);
            }
            else 
            {
                pointer.SetActive(true);
                pointer.transform.LookAt(targetPosition);
                pointerRotation = pointer.transform.rotation;
                pointerRotation.x = 0;
                pointer.transform.rotation = pointerRotation;
            }
            
        }
        
    }

    #endregion
}
