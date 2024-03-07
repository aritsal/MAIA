using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Properties;
using UnityEditor.MPE;
using UnityEngine;

public class DetectHoverables : MonoBehaviour
{
    [field: SerializeField]
    public LayerMask HoverableLayers { get; private set; }
    [field: SerializeField] 
    public float MaxHoverableDistance { get; private set; } = 5f;
    [field: SerializeField]
    public GameObject HoveringObject { get; private set; }
    private IHoverable _hoverableComponent;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        bool hasHit = Physics.Raycast(
            ray: new Ray(transform.position, transform.forward),
            maxDistance: MaxHoverableDistance,
            layerMask: HoverableLayers,
            queryTriggerInteraction: QueryTriggerInteraction.Ignore,
            hitInfo: out RaycastHit hitInfo);
        
        if (!hasHit) {
            _hoverableComponent?.OnHoverExit();
            _hoverableComponent = null;
            HoveringObject = null;
            return;
        }

        if (hitInfo.collider.gameObject.TryGetComponent(out IHoverable newHoverable)) { 
            if (_hoverableComponent == newHoverable) return;
        
            _hoverableComponent?.OnHoverExit();
            newHoverable.OnHoverEnter();
            _hoverableComponent = newHoverable;
            HoveringObject = hitInfo.collider.gameObject;
            return;
        }

        _hoverableComponent?.OnHoverExit();
        _hoverableComponent = null;
        HoveringObject = null;
    }
}
