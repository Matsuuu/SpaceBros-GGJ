using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAttachmentSpawner : MonoBehaviour
{
    public bool spawnsWithBooster;

    public bool spawnsWithTurret;
    public bool spawnsWithSteeringWheel;
    public SpaceShipBuilder.SpaceShipSide side;

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
            attachments.Add(new AttachmentToSpawn("SpaceShipTurret", GetTurretPosition(), GetTurretRotation()));
        }

        if (spawnsWithSteeringWheel)
        {
            attachments.Add(new AttachmentToSpawn("SpaceShipSteeringWheel", new Vector3(1, -2.5f, 0), Quaternion.identity));
        }
    }

    private Vector3 GetTurretPosition()
    {
        switch (side)
        {
            case SpaceShipBuilder.SpaceShipSide.UP:
                return new Vector3(1, -4, 6.5f);
            case SpaceShipBuilder.SpaceShipSide.RIGHT:
                return new Vector3(6, -4, -1);
            case SpaceShipBuilder.SpaceShipSide.DOWN:
                return new Vector3(-1, -4, -7);
            case SpaceShipBuilder.SpaceShipSide.LEFT:
                return new Vector3(-6, -4, 1);
            case SpaceShipBuilder.SpaceShipSide.NONE:
                return Vector3.zero;
            default:
                return Vector3.zero;
        }
    }

    private Quaternion GetTurretRotation()
    {
        switch (side)
        {
            case SpaceShipBuilder.SpaceShipSide.UP:
                return Quaternion.Euler(new Vector3(0, 180, 0));
            case SpaceShipBuilder.SpaceShipSide.RIGHT:
                return Quaternion.Euler(new Vector3(0, -90, 0));
            case SpaceShipBuilder.SpaceShipSide.DOWN:
                return Quaternion.Euler(Vector3.zero);
            case SpaceShipBuilder.SpaceShipSide.LEFT:
                return Quaternion.Euler(new Vector3(0, 90, 0));
            case SpaceShipBuilder.SpaceShipSide.NONE:
                return Quaternion.Euler(Vector3.zero);
            default:
                return Quaternion.Euler(Vector3.zero);
        }
    }
}
