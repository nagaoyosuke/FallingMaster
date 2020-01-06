using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行度やスコアの保存などのフラグ周りをここで管理する(02/20yosuke)
/// </summary>
public static class Save{

    public enum StageState{
        STAGE1,
        STAGE2,
        STAGE3,
        ENDLESS,
        SIMPLESTAGE1,
        SIMPLESTAGE2,
        SIMPLESTAGE3,
    };

    /// <summary>
    /// ステージの進行度
    /// </summary>
    public static StageState stageState = StageState.STAGE1;

    public enum Rank{
        FIRST = 0,
        SECOND = 1,
        THIRD = 2,
        FOURTH = 3,
        FIFTH = 4,
        SIXTH = 5,
        SEVENTH = 6,
        EIGHTH = 7,
        NINTH = 8,
        TENTH = 9,
        MASTER = 10
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
        /// ステージ名やズテージ全体を表示する
        /// </summary>
        STARTCAMERA,
        /// <summary>
        /// 最初に共通の短いカメラ演出
        /// </summary>
        STARTMOVE,
        /// <summary>
        /// タップで始まるのを待つ
        /// </summary>
        STARTWAIT,
        /// <summary>
        /// タップされてから投げられるまで
        /// </summary>
        THROWMOVE,
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
        /// スローモーション終了
        /// </summary>
        SLOWEND,
        /// <summary>
        /// 受け身アニメーション終わったあとの演出開始
        /// </summary>
        UKEMIEFFECT,

        /// <summary>
        /// 追加受け身のスローモーション開始
        /// </summary>
        ADDSLOWSTART,
        /// <summary>
        /// 追加受け身成功失敗時のアニメーション開始
        /// </summary>
        ADDUKEMIANIMETION,
        /// <summary>
        /// 追加受け身入力開始
        /// </summary>
        ADDUKEMI,
        /// <summary>
        /// 追加受け身スローモーション終了
        /// </summary>
        ADDSLOWEND,
        /// <summary>
        /// 追加受け身アニメーション終わったあとの演出開始
        /// </summary>
        ADDUKEMIEFFECT,

        /// <summary>
        /// ２回目以降のカメラ演出開始
        /// </summary>
        MORECAMERA,
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
        NOUKEMI,
        BAD,
        GOOD,
        PERFECT
    };

    /// <summary>
    /// 受け身の評価
    /// </summary>
    public static UkemiRank ukemiRank = UkemiRank.NONE;

    /// <summary>
    /// 受け身入力をしたかどうか
    /// </summary>
    public static bool isUkemi = false;

    /// <summary>
    /// 派生受け身入力をしたかどうか
    /// </summary>
    public static bool isAddUkemi = false;

    public enum AddUkemi{
        NONE,
        GOOD,
        PERFECT,
        NOUKEMI
    };

    /// <summary>
    /// 追加受け身の評価
    /// </summary>
    public static AddUkemi addUkemiRank = AddUkemi.NONE;

    /// <summary>
    /// 受け身入力した回数
    /// ２回目以降に受け身したいときに使えるフラグ
    /// </summary>
    public static int ukemiCounter = 1;

    /// <summary>
    /// Z軸の風の強さ
    /// </summary>
    public static float windZ = 0;

	public static int AddUkemiPoint = 0;

    public static int UkemiPoint = 0;
    public static int AddUkemiPoint_ = 0;

    /// <summary>
    /// エンドレスよう
    /// </summary>
    public static int addUkemiCounter = 0;
    public static int addUkemiCombo = 0;
    public static int addUkemiMaxCombo = 0;

    public static int UkemiScore = 0;
    public static float distance = 0.0f;

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
        AddUkemiPoint_ += AddUkemiPoint;
        switch (Save.ukemiRank)
        {
            case Save.UkemiRank.PERFECT:
                UkemiPoint += 2;
                break;
            case Save.UkemiRank.GOOD:
                UkemiPoint += 1;
                break;
            case Save.UkemiRank.NOUKEMI:
                UkemiPoint += 0;
                break;
        }

        if (UkemiPoint == 6)
        {
            UkemiPoint += 2;
            AddUkemiPoint_ += 3;
        }


        maingameFlag = MainGameFlag.STARTCAMERA;
        ukemiRank = UkemiRank.NONE;
        addUkemiRank = AddUkemi.NONE;
        isUkemi = false;
        ukemiCounter = 1;
        windZ = 0;
		AddUkemiPoint = 0;

        addUkemiCounter = 0;
        addUkemiCombo = 0;
        addUkemiMaxCombo = 0;
        UkemiScore = 0;
        distance = 0.0f;
    }

    /// <summary>
    /// ２回目以降に投げられる前にリセットするときに使う
    /// </summary>
    public static void ThrowReSet()
    {
        ukemiCounter++;
        maingameFlag = MainGameFlag.STARTCAMERA;
        ukemiRank = UkemiRank.NONE;
        addUkemiRank = AddUkemi.NONE;
        isUkemi = false;
    }

    /// <summary>
    /// 追加受け身前にリセットするために使う
    /// </summary>
    public static void AddUkemiReSet()
    {
        ukemiCounter++;
        addUkemiRank = AddUkemi.NONE;
        isAddUkemi = false;
    }

    public static void PointReset()
    {
        UkemiPoint = 0;
        AddUkemiPoint_ = 0;
    }
}
