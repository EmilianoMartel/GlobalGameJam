using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    [SerializeField] private List<GameObject> _npcs;
    [SerializeField] private Vector2 exit;
    [SerializeField] private Vector2 enter;

    private bool is_showing = false;
    private float counter = 0;

    void Start(){

    }

    void Update(){

        counter += Time.deltaTime;;

        if ( is_showing ){
            transform.position = Vector2.Lerp( exit, enter, counter);
        }else {
            transform.position = Vector2.Lerp( enter, exit, counter);
        }
    }
    
    public void ShowNPC(int id){
        HideAll();
        counter = 0.3f;
        is_showing = true;
        _npcs[id].transform.localScale = Vector2.one;
    }

    public void HideNPC(){
        counter = 0;
        is_showing = false;

    }

    private void HideAll(){
        foreach ( var npc in _npcs){
            npc.transform.localScale = Vector2.zero;
        }
    }
}
