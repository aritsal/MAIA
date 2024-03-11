using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Hoverable))]
public class GrowOnHover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private AnimationCurve _ease;
    
    private const float GROW_DURATION = 0.2f;

    private Hoverable _hoverable;
    private void Awake() {
        this._hoverable = this.GetComponent<Hoverable>();
        this._target.localScale = Vector3.zero;
    }

    private void OnEnable() {
        this._hoverable.OnHoverEnter += this.OnHoverEnter;
        this._hoverable.OnHoverExit += this.OnHoverExit;
    }

    private void OnDisable() {
        this._hoverable.OnHoverEnter -= this.OnHoverEnter;
        this._hoverable.OnHoverExit -= this.OnHoverExit;
        this._target.localScale = Vector3.zero;
    }

    private void OnHoverEnter(HoverableDetector _) {
        this.GrowTo(Vector3.one, GROW_DURATION);
    }

    private void OnHoverExit(HoverableDetector _) {
        this.GrowTo(Vector3.zero, GROW_DURATION);
    }

    private Coroutine _currentProcess;
    private void GrowTo(Vector3 targetScale, float duration) {
        if (this._currentProcess != null) {
            this.StopCoroutine(this._currentProcess);
            this._currentProcess = null;
        }
        this._currentProcess = this.StartCoroutine(this.ScaleTo_Coroutine(targetScale, duration));
    }

    private IEnumerator ScaleTo_Coroutine(Vector3 targetScale, float duration) {
        float elapsedTime = 0f;
        Vector3 initialScale = this._target.localScale;
        
        while (elapsedTime < duration) {
            this._target.localScale = Vector3.Lerp(initialScale, targetScale, this._ease.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        this._target.localScale = targetScale;
    }
}
