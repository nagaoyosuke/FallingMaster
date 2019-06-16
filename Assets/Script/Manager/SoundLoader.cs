﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sound.csにse,bgmを読み込ませる
//seの場合はsound.loadse,bgmはsound.loadbgm
//se再生させるときはplayse("hogehogehoege")
//https://qiita.com/2dgames_jp/items/20360f9797c7e8b166bc

//読み込まれない時はファイル名を日本語から英語にしたらなおるときがある。

public class SoundLoader : MonoBehaviour {

	void Awake () {
        //20190228豊田
        //se3種類追加
        //https://soundeffect-lab.info/sound/anime/

        Sound.LoadSe("taiko01", "drum-japanese1");
        Sound.LoadSe("taiko02", "drum-japanese2");
        Sound.LoadSe("osu01", "mens-ou1");
        Sound.LoadSe("dora01", "dora01");
        Sound.LoadSe("syakin", "ukemi_effect_sound01");


        //http://notanomori.net/sound/search/江戸っ子/
        //20190616
        //credit : Nota no Mori 
        Sound.LoadSe("wasshoi", "minoru_voice01");
        Sound.LoadSe("soiya", "minoru_voice02");
        Sound.LoadSe("seiya", "minoru_voice03");
        Sound.LoadSe("yo", "minoru_voice04");
        Sound.LoadSe("hoi", "minoru_voice05");
        Sound.LoadSe("hun", "minoru_voice06");



        Sound.LoadSe("ukemi01", "ukemi01");
        Sound.LoadSe("waterdive", "splash-big");
        Sound.LoadSe("suzume", "passer-montanus-cry1");


        Sound.LoadBgm("Play1", "Play1");
        Sound.LoadBgm("Play2", "Play2");

        Sound.LoadBgm("Result1", "Result1");
        Sound.LoadBgm("Result2", "Result2(Slow)");
        Sound.LoadBgm("Result3", "Result2(Fast)");
        Sound.LoadBgm("Result4", "Result3");


		Sound.LoadSe ("doorclose","扉（閉）");

		Sound.LoadSe("damage01","damage01");
		Sound.LoadSe("damage02","damage02");
		Sound.LoadSe("damage03","damage03");
		Sound.LoadSe("damage04","damage04");//ぽろん系ダメージ
		Sound.LoadSe("damage05","何かにつかえるかも 2");//ぽろん系ダメージ

		Sound.LoadSe ("spinningSlash01","回転斬り");
		Sound.LoadSe ("spinningSlash02","回転斬り１");
		Sound.LoadSe ("spinningSlash03","回転斬り２");
		Sound.LoadSe ("spinningSlash04","回転斬り３");
		Sound.LoadSe ("spinningSlash05","回転斬り４");

		Sound.LoadSe ("chargeGauge01","gagemax1");
		Sound.LoadSe ("chargeGauge02","gagemax2");
		Sound.LoadSe ("chargeGauge03","gagemax3");
		Sound.LoadSe ("chargeGauge04","gagemax4");

		Sound.LoadSe("kabe","壁ぶち当たり");

		Sound.LoadSe("choice","選択音");
		Sound.LoadSe("kettei","決定音");

		Sound.LoadSe("meteo","メテオ");
		Sound.LoadSe("meteobakuha","メテオ爆破");
		Sound.LoadSe ("hokou", "歩行音（一応）");

		//コインゲットするときに使用0826
		//http://taira-komori.jpn.org/game01.html
		Sound.LoadSe ("coin01", "coin02");
		Sound.LoadSe ("coin02", "coin04");
		//https://on-jin.com/sound/sei.php?bunr=割れる&kate=食器
		Sound.LoadSe ("ware", "ware");
		//使ってないフリー素材
		Sound.LoadSe ("WarriorLast", "WarriorLast");






    }
}
