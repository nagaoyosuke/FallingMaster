using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonManager
{
    public class Send
    {
        /// <summary>
        /// スコアを送る時
        /// 段位は数字に置き換える
        /// </summary>
        public class ScoreJson : JsonMethod
        {
            public string state;
            public string unity;
            public int score;

            /// <summary>
            /// 名前
            /// </summary>
            public string name;

            /// <summary>
            /// 各種モード
            /// endless
            /// dani
            /// </summary>
            public string mode;

            public ScoreJson(int score, string name, string mode)
            {
                this.state = "ScoreAppend";
                this.unity = "True";
                this.score = score;
                this.name = name;
                this.mode = mode;
            }
        }

        /// <summary>
        /// その他のAPIを使うよう
        /// </summary>
        public class APIJson : JsonMethod
        {
            /// <summary>
            /// サーバ側に処理してほしいAPIを送る
            /// </summary>
            public  string state;
            public  string unity;

            public APIJson(string state)
            {
                this.state = state;
                this.unity = "True";
            }

        }
    }

    public class Receive
    {
        /// <summary>
        /// スコアを送ったあとにサーバからの返事
        /// </summary>
        public class ScoreJson : JsonMethod
        {
            public  string state;

            public int ranking;

            /// <summary>
            /// 各種モード
            /// endless
            /// dani
            /// </summary>
            public string mode;

            public ScoreJson(int ranking, string mode)
            {
                this.state = "ScoreAppend";
                this.ranking = ranking;
                this.mode = mode;
            }
        }

        /// <summary>
        /// オンラインランキングのベスト5を取得
        /// </summary>
        public class ScoreRankingJson : JsonMethod
        {
            //サーバが判断するために
            public  string state;

            public MemberJson[] dani;
            public MemberJson[] endless;

            public ScoreRankingJson(MemberJson[] dani, MemberJson[] endless)
            {
                this.state = "GetScore";
                this.dani = dani;
                this.endless = endless;
            }
        }

        /// <summary>
        /// オンラインのユーザーを取得
        /// </summary>
        [Serializable]
        public class MemberJson : JsonMethod
        {
            //サーバが判断するために
            public int score;

            /// <summary>
            /// 相手の名前
            /// </summary>
            public string name;


            public MemberJson(int score, string name)
            {
                this.score = score;
                this.name = name;
            }
        }

        /// <summary>
        /// API使った時のサーバからの返事
        /// </summary>
        [Serializable]
        public class APIJson : JsonMethod
        {
            /// <summary>
            /// サーバの返事
            /// </summary>
            public string state;

            public string message;

            public APIJson(string state, string message)
            {
                this.state = state;
                this.message = message;
            }
        }

        /// <summary>
        /// サーバからのエラーメッセージ
        /// </summary>
        public class ErrorJson : JsonMethod
        {
            /// <summary>
            /// サーバの返事
            /// </summary>
            public string state;

            public string message;

            public ErrorJson(string state, string message)
            {
                this.state = state;
                this.message = message;
            }
        }
    }
}

public class JsonMethod
{
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
