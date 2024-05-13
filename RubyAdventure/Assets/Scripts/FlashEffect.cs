using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [Tooltip("Material to switch to during the flash.")] [SerializeField]
    private Material flashMaterial;

    [Tooltip("Duration of the flash.")] [SerializeField]
    private float duration;

    #endregion

    #region Private Fields

    // The SpriteRenderer that should flash.
    private SpriteRenderer _spriteRenderer;

    // The material that was in use, when the script started.
    private Material _originalMaterial;

    // The currently running coroutine.
    private Coroutine _flashRoutine;

    #endregion

    #endregion


    #region Methods

    #region Unity Callbacks

    private void Start()
    {
        // Get the SpriteRenderer to be used,
        // alternatively you could set it from the inspector.
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the material that the SpriteRenderer uses, 
        // so we can switch back to it after the flash ended.
        _originalMaterial = _spriteRenderer.material;
    }

    #endregion

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (_flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(_flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        _spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        _spriteRenderer.material = _originalMaterial;

        // Set the routine to null, signaling that it's finished.
        _flashRoutine = null;
    }

    #endregion
}