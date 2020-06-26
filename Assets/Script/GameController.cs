using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] invSlots;
    public UsableItem[] invItems;
    public UsableItem[] items;
    bool[] gotItem;
    public static int c = 0;

    public GameObject sp1, sp2, sp3;

    public Sprite defaultSprite;

    public Text scoreText;
    static int coinsAmount = 50;

    bool dragging = false;
    public bool firstDrag = true;
    UsableItem draggObj;
    GameObject draggingObj;

    public LayerMask groundMask;

    public static UsableItem[] backup;
    public static bool doBackUp = false;

    public Animator fadeAnim;
    public GameObject applesPanel;
    public bool firstTime = true;

    #region Singleton
    public static GameController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        gotItem = new bool[items.Length];
        invItems = new UsableItem[items.Length];
        scoreText.text = "x" + coinsAmount;
        if (doBackUp)
        {
            invItems = backup;
            for (int i = 0; i < invItems.Length; i++)
            {
                if (invItems[i] != null)
                {
                    invSlots[i].transform.GetChild(0).gameObject.SetActive(true);
                    invSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = invItems[i].sprite;
                }
            }
            doBackUp = false;
        }
    }

    public void GenerateItem()
    {
        if (coinsAmount >= 5)
        {
            if (c < items.Length)
            {
                int rnd;
                do
                {
                    rnd = Random.Range(0, items.Length);
                } while (gotItem[rnd]);
                gotItem[rnd] = true;
                invItems[c] = items[rnd];
                invSlots[c].transform.GetChild(0).gameObject.SetActive(true);
                invSlots[c].transform.GetChild(0).GetComponent<Image>().sprite = items[rnd].sprite;
                c++;

                coinsAmount -= 5;
                scoreText.text = "x" + coinsAmount;

            }
            else
            {
                sp1.SetActive(false);
                sp3.SetActive(true);

            }
        }
        else
        {
            sp1.SetActive(false);
            sp2.SetActive(true);
        }
    }

    public void GiveItem(UsableItem item)
    {
        invItems[c] = item;
        invSlots[c].transform.GetChild(0).gameObject.SetActive(true);
        invSlots[c].transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        c++;
    }

    public bool GiveCompost(UsableItem item)
    {
        if (coinsAmount >= 30)
        {
            GiveItem(item);
            coinsAmount -= 30;
            scoreText.text = "x" + coinsAmount;
            return true;
        }
        else
        {
            SetStuck();
            return false;
        }
    }

    public void SetStuck()
    {
        GameObject.FindGameObjectWithTag("OldMan").GetComponent<OldMan>().SetStuck();
    }

    public void SetGotIt()
    {
        GameObject.FindGameObjectWithTag("OldMan").GetComponent<OldMan>().SetGotIt();
    }

    public void SeeTree(bool seeing)
    {
        applesPanel.SetActive(seeing);
        if (firstTime && seeing)
        {
            SetGotIt();
        }
    }

    public void NextScene()
    {
        doBackUp = true;
        backup = invItems;
        StartCoroutine(WaitScene());
    }

    IEnumerator WaitScene()
    {
        fadeAnim.SetBool("Outro", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Win()
    {
        fadeAnim.SetBool("Mediate", true);
        StartCoroutine(HideNPC());
    }

    IEnumerator HideNPC()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("OldMan").SetActive(false);
        GameObject.FindGameObjectWithTag("Farmer").SetActive(false);
    }

    public void Lose()
    {
        fadeAnim.SetBool("LoseOutro", true);
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(10);
        doBackUp = false;
        backup = null;
        coinsAmount = 0;
        SceneManager.LoadScene(0);
    }

    public void OnDrop(GameObject go)
    {
        UsableItem item = GetItem(go);
        if (Dropper.instance.OnDrop())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, item.mask))
            {
                Instantiate(item.obj, hit.transform.position, item.obj.transform.rotation);
            }
        }
    }

    public UsableItem GetItem(GameObject gameObject)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (gameObject == invSlots[i])
            {
                return invItems[i];
            }
        }
        return null;
    }

    public void ScoreUp()
    {
        coinsAmount++;
        scoreText.text = "x" + coinsAmount;
    }

    public void SetDragging(bool dragging, GameObject obj)
    {
        this.dragging = dragging;
        this.draggObj = GetItem(obj);
    }

    

    private void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, draggObj.mask))
            {
                if (firstDrag)
                {
                    draggingObj = Instantiate(draggObj.obj, hit.transform.position, draggObj.obj.transform.rotation);
                    draggingObj.GetComponent<Collider>().isTrigger = true;
                    firstDrag = false;
                }
                else if (draggingObj != null)
                {
                    draggingObj.transform.position = hit.transform.position;
                }
            }
            else if (Physics.Raycast(ray, out hit, 1000, groundMask))
            {
                if (firstDrag)
                {
                    draggingObj = Instantiate(draggObj.obj, hit.point, draggObj.obj.transform.rotation);
                    draggingObj.GetComponent<Collider>().isTrigger = true;
                    firstDrag = false;
                }
                else if (draggingObj != null)
                {
                    draggingObj.transform.position = hit.point;
                }
            }
            else
            {
                Destroy(draggingObj);
                firstDrag = true;
            }
        }
        else if (!firstDrag)
        {
            Destroy(draggingObj);
            firstDrag = true;
        }

    }
}



