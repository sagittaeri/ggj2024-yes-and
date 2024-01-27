using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Threading;

public class GachaCapsuleRandomiser : MonoBehaviour
{
    public Transform enclosure;
    public Transform breakRoot;
    public float breakRotateInterval = 0.5f;
    public Transform breakableTopHalf;
    public Transform breakableMid;
    public Transform breakableBottomHalf;
    public MeshRenderer bottomRenderer;
    public Transform effectRoot;
    public Transform effect;
    public Transform paper;
    public TextMeshPro nameText;
    public TextMeshPro descriptionText;
    public GameObject[] contentList;
    public Material[] materialList;

    public GameObject content;
    public bool doContentAnimate = false;
    public float contentRotate = 37.5f;

    private float timeRotate = -1f;
    private Vector3 topPos;
    private Vector3 bottomPos;
    private Vector3 topAngle;
    private Vector3 bottomAngle;
    private bool contentLeft;
    private bool initialised;

    void Awake()
    {
        if (initialised)
            return;
        initialised = true;
        if (breakRoot != null)
            breakRoot.gameObject.SetActive(false);
        if (breakableTopHalf != null)
        {
            topPos = breakableTopHalf.localPosition;
            topAngle = breakableTopHalf.localEulerAngles;
        }
        if (breakableBottomHalf != null)
        {
            bottomPos = breakableBottomHalf.localPosition;
            bottomAngle = breakableBottomHalf.localEulerAngles;
        }
        // Randomise();
    }

    void Update()
    {
        if (effect != null && timeRotate < Time.timeSinceLevelLoad)
        {
            timeRotate = Time.timeSinceLevelLoad + breakRotateInterval;
            effect.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));

            if (doContentAnimate)
            {
                Vector3 newAngle = content.transform.localEulerAngles;
                newAngle.z = contentLeft ? -contentRotate : -contentRotate * 3f;
                content.transform.localEulerAngles = newAngle;
                contentLeft = !contentLeft;
            }
        }
    }

    public string Randomise()
    {
        gameObject.SetActive(true);
        if (!initialised)
            Awake();
        gameObject.SetActive(false);
        if (contentList == null  || contentList.Length == 0)
            return null;
        if (content != null)
            Destroy(content);
        int chosenIndex = Random.Range(0, contentList.Length);
        bottomRenderer.material = materialList[chosenIndex];

        GameObject chosenOne = contentList[chosenIndex];
        content = Instantiate(chosenOne, enclosure);
        content.transform.localPosition = Vector3.zero;
        content.transform.localEulerAngles = Vector3.zero;

        if (enclosure != null)
        {
            enclosure.DOKill();
            enclosure.localScale = Vector3.one;
        }
        if (breakableTopHalf != null)
        {
            breakableTopHalf.DOKill();
            breakableTopHalf.localPosition = topPos;
        }
        if (breakableBottomHalf != null)
        {
            breakableBottomHalf.DOKill();
            breakableBottomHalf.localPosition = bottomPos;
        }
        if (breakableMid != null)
            breakableMid.gameObject.SetActive(true);
        return chosenOne.name;
    }

    public void BreakOpen()
    {
        enclosure.DOKill();
        enclosure.DOScaleY(0.8f, 0.3f).SetEase(Ease.OutQuad).OnComplete(()=>
        {
            enclosure.DOScaleY(1f, 0.2f).SetEase(Ease.OutQuad);
            DOVirtual.DelayedCall(0.1f, ()=>
            {
                if (breakableMid != null)
                    breakableMid.gameObject.SetActive(false);
            });
            if (breakableTopHalf != null)
            {
                breakableTopHalf.DOKill();
                breakableTopHalf.DOLocalMoveY(topPos.y + 3f, 0.3f);
            }
            if (breakableBottomHalf != null)
            {
                breakableBottomHalf.DOKill();
                breakableBottomHalf.DOLocalMoveY(topPos.y - 3f, 0.3f);
            }
        });
    }
}
