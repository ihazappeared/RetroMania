using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public GameObject destroyObj;

	void Start ()
    {
        destroyObj.SetActive(true);
        destroyObj.GetComponent<Animator>().SetTrigger("Start");

    }
}
