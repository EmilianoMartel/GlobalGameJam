using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _npcs;
    [SerializeField] private Transform exit;
    [SerializeField] private Transform enter;
    [SerializeField] private float _speed = 1.5f;

    private Coroutine currentCoroutine;
    private int _currentID;

    private void Start()
    {
        HideAll();
    }

    public void ShowNPC(int id)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        _currentID = id;
        currentCoroutine = StartCoroutine(MoveNPC(_npcs[id], exit.position, enter.position, true));
    }

    public void HideNPC()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        for (int i = 0; i < _npcs.Count; i++)
        {
            if(_currentID == i)
            {
                currentCoroutine = StartCoroutine(MoveNPC(_npcs[i], enter.position, exit.position, false));
            }
        }
    }

    private IEnumerator MoveNPC(GameObject npc, Vector3 from, Vector3 to, bool show)
    {
        npc.gameObject.SetActive(true);

        float counter = 0;

        while (counter < 1)
        {
            counter += Time.deltaTime * _speed;
            npc.transform.position = Vector3.Lerp(from, to, counter);
            yield return null;
        }

        npc.transform.position = to;

        if (!show) npc.transform.localScale = Vector2.zero;
    }

    private void HideAll()
    {
        foreach (var npc in _npcs)
        {
            npc.gameObject.SetActive(false);
            npc.transform.position = exit.position;
        }
    }
}
