using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowController : MonoBehaviour
{
    public BowController Instance { get; private set; }

    enum Handedness { Right, Left };

    [SerializeField] Transform RightController;
    [SerializeField] Transform LeftController;

    [SerializeField] GameObject BowPrefab;
    [SerializeField] GameObject ArrowPrefab;

    [SerializeField] Handedness handedness;
    [SerializeField] bool ShowInteractor;

    [Header("Arrow Launch Settings")]
    [Range(0.0f, 5.0f)] public float ForceMultiplier = 1;

    GameObject Bow;
    XRSimpleInteractable BowInteractable;
    float drawDistance = 0;

    void Awake()
    {
        Instance = this;

        Bow = Instantiate(BowPrefab);
        BowInteractable = Bow.GetComponentInChildren<XRSimpleInteractable>();
        BowInteractable.selectExited.AddListener((SelectExitEventArgs) => { LaunchArrow(drawDistance); });
    }

    void Update()
    {
        Bow.transform.localPosition = Vector3.zero;
        Bow.transform.localRotation = Quaternion.identity;

        Bow.transform.parent = (handedness == Handedness.Right) ?
            LeftController :
            RightController;

        BowInteractable.interactionLayers = (handedness == Handedness.Right) ?
            InteractionLayerMask.GetMask("Only Right") :
            InteractionLayerMask.GetMask("Only Left");

        drawDistance = BowInteractable.isSelected ? Vector3.Distance(Bow.transform.position, BowInteractable.interactorsSelecting[0].transform.position) : 0;
        drawDistance = Mathf.Clamp(drawDistance, 0, 1);

        BowInteractable.GetComponent<MeshRenderer>().material.color = ShowInteractor ? (1 - drawDistance) * Vector4.one : Vector4.zero;
    }

    public void LaunchArrow(float force)
    {
        GameObject arrow = Instantiate(ArrowPrefab);

        arrow.transform.position = Bow.transform.position;
        arrow.transform.forward = Bow.transform.forward;

        arrow.GetComponent<Rigidbody>().velocity = ForceMultiplier * 20 * (force + 0.2f) * arrow.transform.forward;
    }
}
