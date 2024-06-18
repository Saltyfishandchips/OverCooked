using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounterAnimator : MonoBehaviour
{
    const string OPEN_CLOSE = "OpenClose";
    private Animator animator;

    [SerializeField] private ContainerCounter containerCounter;

    private void Awake() {
        TryGetComponent(out animator);
        containerCounter.OnCounterOpenClose += ContainerCounterOpenCloseAnim;
    }

    private void ContainerCounterOpenCloseAnim(object sender, EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
