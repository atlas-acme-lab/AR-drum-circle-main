using NRKernal;
using UnityEngine;

public class TrackableFoundTest : MonoBehaviour
{
    public TrackableObserver Observer;
    public GameObject Obj;
    void Start()
    {
#if !UNITY_EDITOR
        Destroy(GameObject.Find("EmulatorRoom"));
#endif
        Obj.SetActive(false);
        Observer.FoundEvent += Found;
        Observer.LostEvnet += Lost;
    }

    private void Update()
    {
        if (NRInput.GetButtonDown(ControllerButton.TRIGGER))
            Obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Found(Vector3 pos, Quaternion qua)
    {
        //Obj.transform.position = -pos + new Vector3(0, +0.125f, 0);
        Obj.transform.rotation = Quaternion.Inverse(qua);
        Obj.SetActive(true);
    }
    private void Lost()
    {
        Obj.SetActive(false);
    }
}