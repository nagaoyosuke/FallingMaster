using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受け身する場所によって処理を変えるためのインターフェース(06/11長尾)
/// </summary>
public interface IUkemiEffect
{
    void StartEffect();
    void EndEffect();
    void PerfectEffect();
    void GoodEffect();
    void BadEffect();
    void FailureNoUkemiEffect();
}
