using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using DG.Tweening;

public class CameraFollowScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform _player;
    public Transform Player
    {
        get => _player;
        set => _player = value;
    }

    private Vector3 moreOffset = Vector3.zero;
    private Tween tween;

    [SerializeField] private Vector3 _camOffset = new Vector3(0, 1, -5);
    // Update is called once per frame
    void Update ()
    {
        if (Player != null)
            transform.position = _player.transform.position + _camOffset + moreOffset;
    }
    
    public void PullBack()
    {
        tween?.Kill();
        tween = DOVirtual.Vector3(moreOffset, new Vector3(10f, 50f, -50f), 1.5f, (Vector3 val)=>
        {
            moreOffset = val;
        }).SetEase(Ease.OutSine);
    }

    public void ReturnToNormal()
    {
        tween?.Kill();
        tween = DOVirtual.Vector3(moreOffset, Vector3.zero, 0.5f, (Vector3 val)=>
        {
            moreOffset = val;
        }).SetEase(Ease.OutSine);
    }
}
