using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAttachmentSpawner : MonoBehaviour
{
    public bool spawnsWithBooster;

    public bool spawnsWithTurret;
    public bool spawnsWithSteeringWheel;

    private GameObject attachment;

    private List<AttachmentToSpawn> attachments = new List<AttachmentToSpawn>();

    private Vector3 attachmentOffset = Vector3.zero;
    private Quaternion attachmentRotation = Quaternion.identity;

    public struct AttachmentToSpawn
    {
        public string name { get; set; }
        public Vector3 offset { get; set; }
        public Quaternion rotation { get; set; }

        public AttachmentToSpawn(string name, Vector3 offset, Quaternion rotation)
        {
            this.name = name;
            this.offset = offset;
            this.rotation = rotation;
        }
    }
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
        if (attachments.Count < 1)
        {
            return;
        }
        attachments.ForEach(attachmentToSpawn =>
        {
            attachment = Resources.Load<GameObject>("Prefabs/" + attachmentToSpawn.name);
            Instantiate(attachment, transform.position + attachmentToSpawn.offset, attachmentToSpawn.rotation, transform);
        });
    }

    private void SetAttachmentSpecs()
    {
        if (spawnsWithBooster)
        {
            attachmentOffset = new Vector3(-(transform.localScale.x), 0, 0);
            attachmentRotation = Quaternion.Euler(new Vector3(0,0,90));
            attachments.Add(new AttachmentToSpawn("SpaceShipBooster", attachmentOffset, attachmentRotation));
        }

        if (spawnsWithTurret)
        {
            attachments.Add(new AttachmentToSpawn("SpaceShipTurret", Vector3.zero, Quaternion.identity));
        }

        if (spawnsWithSteeringWheel)
        {
            attachments.Add(new AttachmentToSpawn("SpaceShipSteeringWheel", new Vector3(1, -2.5f, 0), Quaternion.identity));
        }
    }
}
