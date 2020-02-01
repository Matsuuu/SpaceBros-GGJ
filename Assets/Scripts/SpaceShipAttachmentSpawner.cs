using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAttachmentSpawner : MonoBehaviour
{
    public bool spawnsWithBooster;

    public bool spawnsWithTurret;

    private GameObject attachment;

    private string attachmentName;

    private Vector3 attachmentOffset = Vector3.zero;
    private Quaternion attachmentRotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        SetAttachmentSpecs();
        if (attachmentName == null)
        {
            return;
        }
        attachment = Resources.Load<GameObject>("Prefabs/" + attachmentName);
        Instantiate(attachment, transform.position + attachmentOffset, attachmentRotation, transform);
    }

    private void SetAttachmentSpecs()
    {
        if (spawnsWithBooster)
        {
            attachmentName = "SpaceShipBooster";
            attachmentOffset = new Vector3(-(transform.localScale.x), 0, 0);
            attachmentRotation = Quaternion.Euler(new Vector3(0,0,90));
        }

        if (spawnsWithTurret)
        {
            attachmentName = "SpaceShipTurret";
        }
    }
}
