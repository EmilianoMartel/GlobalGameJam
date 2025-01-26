using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] float _direction = -1f;
    [SerializeField] private List<GameObject> _backGroundList;
    [SerializeField] private float _finalPositionX = -10;

    private float _backgroundWidth;

    private void Start()
    {
        if (_backGroundList.Count > 0)
        {
            _backgroundWidth = _backGroundList[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    private void Update()
    {
        MoveLogic();
    }

    private void MoveLogic()
    {
        for (int i = 0; i < _backGroundList.Count; i++)
        {
            _backGroundList[i].transform.Translate(_moveSpeed * _direction * Time.deltaTime, 0, 0, Space.World);

            if (_backGroundList[i].transform.position.x <= -_backgroundWidth)
            {
                GameObject rightmostBackground = GetRightmostBackground();

                float newXPosition = rightmostBackground.transform.position.x + _backgroundWidth;

                _backGroundList[i].transform.position = new Vector3(newXPosition, _backGroundList[i].transform.position.y, _backGroundList[i].transform.position.z);
            }
        }
    }

    private GameObject GetRightmostBackground()
    {
        GameObject rightmost = _backGroundList[0];

        foreach (var background in _backGroundList)
        {
            if (background.transform.position.x > rightmost.transform.position.x)
            {
                rightmost = background;
            }
        }

        return rightmost;
    }

}