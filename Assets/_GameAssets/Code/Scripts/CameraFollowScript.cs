using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform _player;

    [SerializeField] private Vector3 _camOffset = new Vector3(0, 1, -5);
    // Update is called once per frame
    void Update ()
    {
        transform.position = _player.transform.position + _camOffset;
    }
}
