using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SocketGameProtocol;


public class PlayerManager :SingletonMono<PlayerManager>
{


    //private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    //private GameObject chararcter;
    //private GameObject arrow;
    //private Transform spawnPos;

    //public void Start()
    //{
    //    //加载
    //    chararcter = Resources.Load("Prefab/Character") as GameObject;
    //    bullet = Resources.Load("Prefab/bullet") as GameObject;
    //}

    //public string CurPlayerID
    //{
    //    get;
    //    set;
    //}

    //public void addPlayer(MainPack pack)
    //{
    //    int posindex = -4;
    //    spawnPos = GameObject.Find("SpawnPos").transform;
    //    foreach (var p in pack.PlayerPack)
    //    {
    //        Debug.Log("添加角色" + p.PlayerName);
    //        Vector3 pos = spawnPos.position;
    //        pos.x = posindex++;
    //        GameObject g = GameObject.Instantiate(chararcter, pos, Quaternion.identity);
    //        if (p.PlayerName.Equals(face.UserName))
    //        {
    //            //创建本地角色
    //            g.AddComponent<Rigidbody2D>().gravityScale = 3;
    //            g.AddComponent<UpPosRequest>();
    //            g.AddComponent<UpPos>();
    //            g.AddComponent<CharacterController>();

    //            CharacterRistic characterRistic = g.AddComponent<CharacterRistic>();
    //            characterRistic.isLocal = true;
    //            characterRistic.username = p.Playername;

    //            FireRequest fireRequest = g.transform.Find("HandGun").gameObject.AddComponent<FireRequest>();
    //            DamageRequest damageRequest = g.transform.Find("HandGun").gameObject.AddComponent<DamageRequest>();

    //            GunController gunController = g.transform.Find("HandGun").gameObject.AddComponent<GunController>();
    //            gunController.fireRequest = fireRequest;
    //            gunController.damageRequest = damageRequest;
    //        }
    //        else
    //        {
    //            //创建其他客户端的角色
    //            CharacterRistic characterRistic = g.AddComponent<CharacterRistic>();
    //            characterRistic.username = p.Playername;

    //            g.AddComponent<RemoteCharacter>();
    //        }
    //        players.Add(p.Playername, g);
    //    }
    //}

    //public void removePlayer(string id)
    //{
    //    if (players.TryGetValue(id, out GameObject g))
    //    {
    //        GameObject.Destroy(g);
    //        players.Remove(id);
    //    }
    //    else
    //    {
    //        Debug.Log("移除角色出错！");
    //    }
    //}

    //public void GameExit()
    //{
    //    foreach (var VARIABLE in players.Values)
    //    {
    //        GameObject.Destroy(VARIABLE);
    //    }
    //    players.Clear();
    //}


    //public void UpPos(MainPack pack)
    //{

    //    PosPack posPack = pack.Playerpack[0].Pospack;

    //    if (players.TryGetValue(pack.Playerpack[0].Playername, out GameObject g))
    //    {

    //        Vector2 Pos = new Vector2(posPack.PosX, posPack.PosY);//角色位置
    //        float CharacterRot = posPack.RotZ;
    //        float GunRot = posPack.GunRotZ;

    //        g.GetComponent<RemoteCharacter>().SetState(Pos, CharacterRot, GunRot);
    //        // g.transform.eulerAngles=new Vector3(0,0,CharacterRot);
    //        // g.transform.Find("HandGun").eulerAngles=new Vector3(0,0,GunRot);

    //    }
    //    if (pack.Playerpack[0].Playername == "1234")
    //    {
    //        face.isRec = true;

    //    }
    //}

    //public void spawnBullet(MainPack pack)
    //{
    //    Vector3 pos = new Vector3(pack.Bulletpack.PosX, pack.Bulletpack.PosY, 0);
    //    float rot = pack.Bulletpack.RotZ;
    //    Vector3 mousepos = new Vector3(pack.Bulletpack.MousePosX, pack.Bulletpack.MousePosY, 0);
    //    Vector3 velocity = (mousepos - pos).normalized * 20;
    //    GameObject g = GameObject.Instantiate(bullet, pos, Quaternion.identity);
    //    g.transform.eulerAngles = new Vector3(0, 0, rot);
    //    g.GetComponent<Rigidbody2D>().velocity = velocity;
    //}

    //public void Damage(MainPack pack)
    //{
    //    if (pack.Playerpack[0].Playername == face.UserName)
    //    {
    //        Vector2 start = new Vector2(pack.Playerpack[0].Pospack.PosX, pack.Playerpack[0].Pospack.PosY);
    //        Vector2 end = new Vector2(pack.Playerpack[0].Pospack.RotZ, pack.Playerpack[0].Pospack.GunRotZ);
    //        if (players.TryGetValue(pack.Playerpack[0].Playername, out GameObject g))
    //        {
    //            g.GetComponent<Rigidbody2D>().velocity += (start - end).normalized * 5;
    //        }
    //    }
    //}
}