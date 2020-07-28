using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utils {
    public class MapMethods : MonoBehaviour {

        public Sprite mapSprite(int map, bool isBossMap) {
            map = map - 1;
            List<Sprite> mapSprites = new List<Sprite>();

            mapSprites.Add(getSprite("Forest"));
            mapSprites.Add(getSprite("Beach"));
            mapSprites.Add(getSprite("Ruins"));
            mapSprites.Add(getSprite("Mountain"));
            mapSprites.Add(getSprite("Vulcanic"));
            mapSprites.Add(getSprite("ForestBoss"));
            mapSprites.Add(getSprite("BeachBoss"));
            mapSprites.Add(getSprite("RuinsBoss"));
            mapSprites.Add(getSprite("MountainBoss"));
            mapSprites.Add(getSprite("VulcanicBoss"));

            if (isBossMap)
                map = map + 5;

            return mapSprites[map];
        }
        public string mapName(int map, bool isBossMap) {
            map = map - 1;

            List<string> mapsName = new List<string>();
            mapsName.Add("Floresta");
            mapsName.Add("Praia");
            mapsName.Add("Ruínas");
            mapsName.Add("Montanha");
            mapsName.Add("Vulcão");
            mapsName.Add("FlorestaChefe");
            mapsName.Add("PraiaChefe");
            mapsName.Add("RuínasChefe");
            mapsName.Add("MontanhaChefe");
            mapsName.Add("VulcãoChefe");

            if (isBossMap)
                map = map + 5;

            return mapsName[map];
        }
        public AudioClip mapMusic(string sceneName) {
            string name = "";
            int map = ApplicationModel.CurrentWorldMap;
            bool isBossMap = false;

            if (ApplicationModel.BattleType == BattleType.BOSS_BATTLE)
                isBossMap = true;
            if (sceneName == "WorldMap") {
                name = mapName(map, isBossMap);
            } else if (sceneName != "Battle") {
                name = "Generic";
            }else {
                if (isBossMap && map == 5) {
                    name = "FinalBoss";
                } else if (isBossMap) {
                    name = "Boss";
                } else {
                    name = "Battle";
                }
            }
                return getClip(name);
            }
            AudioClip getClip(string name) {
                return Resources.Load<AudioClip>("Sounds/" + name);
            }
            Sprite getSprite(string name) {
                return Resources.Load<Sprite>("SpritesSpeaks/Background/" + name);
            }
        }
    }
