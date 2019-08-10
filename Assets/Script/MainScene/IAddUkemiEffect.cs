﻿/// <summary>
/// 追加受け身する場所によって処理を変えるためのインターフェース(06/11長尾)
/// </summary>
public interface IAddUkemiEffect
{
    void AddStartEffect();
    void AddEndEffect();
    void AddPerfectEffect();
    void AddFailureNoUkemiEffect();
}