using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    private Animator animator;

    const string CUT = "Cut";

    private void Start() {
        animator = GetComponent<Animator>();
        cuttingCounter.OnCuttingProgressAnimator += ProgressAnimaNotify;
    }

    private void ProgressAnimaNotify(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
