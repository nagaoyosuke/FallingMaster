﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行度やスコアの保存などのフラグ周りをここで管理する(02/20yosuke)
/// </summary>
public static class Save{

    public enum StageState{
        STAGE1,
        STAGE2,
        STAGE3
    };

    /// <summary>
    /// ステージの進行度
    /// </summary>
    public static StageState stageState = StageState.STAGE1;

    public enum Rank{
        FIRST,
        SECOND,
        THIRD,
        FOURTH,
        FIFTH,
        SIXTH,
        SEVENTH,
        EIGHTH,
        NINTH,
        TENTH,
        MASTER
    };

    /// <summary>
    /// 段位の記録場所
    /// </summary>
    public static Rank rank = Rank.FIRST;

    public static Rank HightRank = Rank.FIRST;

    /// <summary>
    ///02/20の時点で仮決めされてるのを書いたから変更されるかも(02/20yosuke)
    /// </summary>
    public enum MainGameFlag{
        /// <summary>
        /// 最初に共通の短いカメラ演出
        /// </summary>
        STARTCAMERA,
        /// <summary>
        /// タップで始まるのを待つ
        /// </summary>
        STARTWAIT,
        /// <summary>
        /// 投げられてる時
        /// </summary>
        THROW,
        /// <summary>
        /// 落ちてからスローモーションになるまで
        /// </summary>
        FALLING,
        /// <summary>
        /// スローモーション開始
        /// </summary>
        SLOWSTART,
        /// <summary>
        /// 受け身入力開始
        /// </summary>
        UKEMI,
        /// <summary>
        /// 受け身成功失敗時のアニメーション開始
        /// </summary>
        UKEMIANIMETION,
        /// <summary>
        /// 派生受け身入力開始
        /// </summary>
        ADDUKEMI,
        /// <summary>
        /// スローモーション終了
        /// </summary>
        SLOWEND,
        /// <summary>
        /// スローモーション終了後の派生アニメーション開始
        /// </summary>
        ENDANIMETION,
        /// <summary>
        /// スローモーションから結果表示までの間
        /// </summary>
        ENDCAMERA,
        /// <summary>
        /// リザルト表示
        /// </summary>
        RESULT,
    };

    /// <summary>
    /// メインゲームの進行度
    /// </summary>
    public static MainGameFlag maingameFlag = MainGameFlag.STARTCAMERA;

    public enum UkemiRank{
        NONE,
        BAD,
        GOOD,
        PERFECT
    };

    /// <summary>
    /// 受け身の評価
    /// </summary>
    public static UkemiRank ukemiRank = UkemiRank.NONE;

    public enum AddUkemi{
        NONE,
        CENTER,
        RIGHT,
        LEFT,
        BACK
    };

    /// <summary>
    /// 派生受け身の方向
    /// </summary>
    public static AddUkemi addUkemi = AddUkemi.NONE;

    /// <summary>
    /// ハイスコア以外を初期化する。初めから遊ぶときに使う
    /// </summary>
    public static void ReSet(){
        stageState = StageState.STAGE1;
        rank = Rank.FIRST;
        FlagReSet();
    }

    /// <summary>
    /// メインゲームの初期化,ステージ変わった時とかに使う
    /// </summary>
    public static void FlagReSet(){
        maingameFlag = MainGameFlag.STARTCAMERA;
        ukemiRank = UkemiRank.NONE;
        addUkemi = AddUkemi.NONE;
    }
}
