using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{    
    protected override void Awake() //attackInfo
    {
        AttackInfo[] tempInfos = new AttackInfo[5];

        //평타1
        tempInfos[0].attackRange = new Vector2(1f, 1f);
        tempInfos[0].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.15f;
        tempInfos[0].preDelay = 0f;
        tempInfos[0].postDelay = 0.1f;
        tempInfos[0].attackID = "attack1";


        //평타2
        tempInfos[1].attackRange = new Vector2(1f, 1f);
        tempInfos[1].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[1].damage = 1;
        tempInfos[1].duration = 0.15f;
        tempInfos[1].preDelay = 0f;
        tempInfos[1].postDelay = 0.1f;
        tempInfos[1].attackID = "attack2";


        //평타3
        tempInfos[2].attackRange = new Vector2(1f, 1f);
        tempInfos[2].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[2].damage = 1;
        tempInfos[2].duration = 0.15f;
        tempInfos[2].preDelay = 0f;
        tempInfos[2].postDelay = 0.1f;
        tempInfos[2].attackID = "attack_skill1";


        //대쉬 공격
        tempInfos[3].attackRange = new Vector2(5f, 1f);
        tempInfos[3].hitBoxPostion = new Vector2(-2.5f, 0f);
        tempInfos[3].damage = 1;
        tempInfos[3].duration = 0.2f;
        tempInfos[3].preDelay = 0f;
        tempInfos[3].postDelay = 0.1f;
        tempInfos[3].attackID = "dashAttack";


        //점프 스킬
        tempInfos[4].attackRange = new Vector2(1f, 0.5f);
        tempInfos[4].hitBoxPostion = new Vector2(0.5f, -0.5f);
        tempInfos[4].damage = 1;
        tempInfos[4].duration = 10f;
        tempInfos[4].preDelay = 0f;
        tempInfos[4].postDelay = 0.1f;

        foreach (var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    private int atkCount;
    private float isAtkCounting;

    private void Update()
    {
        if (isAtkCounting > 0) isAtkCounting -= Time.deltaTime;
        if (atkCount > 0 && isAtkCounting <= 0) atkCount = 0;
    }

    public override void Action(float atk, float atkSpd) // 최종적으로 이걸로 공격함
    {
        SoundDelegate.Instance.PlayEffectSound((EffectSoundType)atkCount);
        AttackInfo tempInfo = attackInfos[atkCount];
        tempInfo.damage += atk;
        tempInfo.duration *= atkSpd;
        tempInfo.preDelay *= atkSpd;
        tempInfo.postDelay *= atkSpd;

        StartCoroutine(Action_Attack(tempInfo));

        atkCount++;
        if (atkCount > 2)
            atkCount = 0;
        else if (atkCount >= 2 && !PlayManager.Instance.Player.IsGround)
            atkCount = 0;
        isAtkCounting = 0.5f;
    }

    public override void DashAttack(float atk, float atkSpd)
    {
        AttackInfo tempInfo = attackInfos[3];
        tempInfo.damage += atk;
        tempInfo.duration *= atkSpd;
        tempInfo.preDelay *= atkSpd;
        tempInfo.postDelay *= atkSpd;
        StartCoroutine(Action_DashAttack(tempInfo));
    }    

    public override void JumpSkillAction(float atk, float atkSpd)
    {
        AttackInfo tempInfo = attackInfos[4];
        tempInfo.damage += atk;
        tempInfo.duration *= atkSpd;
        tempInfo.preDelay *= atkSpd;
        tempInfo.postDelay *= atkSpd;
        StartCoroutine(Action_JumpSkill(tempInfo));
    }

    IEnumerator Action_Attack(AttackInfo info)
    {
        onAttack = true;
        PlayManager.Instance.Player.IsMovable = false;

        PlayManager.Instance.Player.anim.Play(info.attackID, 0, 0);

        yield return new WaitForSeconds(info.preDelay);

        CombatSystem.Instance.InstantiateHitBox(info, gameObject.transform);

        yield return new WaitForSeconds(info.postDelay + info.duration);

        onAttack = false;
        PlayManager.Instance.Player.IsMovable = true;

    }

    IEnumerator Action_DashAttack(AttackInfo info)
    {
        onAttack = true;
        PlayManager.Instance.Player.IsMovable = false;

        yield return new WaitForSeconds(info.preDelay);

        CombatSystem.Instance.InstantiateHitBox(info, gameObject.transform);

        yield return new WaitForSeconds(info.postDelay + info.duration);

        onAttack = false;
        PlayManager.Instance.Player.IsMovable = true;
    }

    IEnumerator Action_JumpSkill(AttackInfo info)
    {
        onAttack = true;
        PlayManager.Instance.Player.IsMovable = false;
        PlayManager.Instance.Player.stopJumpSkill = false;

        string path = "Prefabs/Colliders/JumpSkillCollider";

        GameObject col = CombatSystem.Instance.InstantiateHitBox(info, gameObject.transform, path);

        Vector2 vec = PlayManager.Instance.Player.Direction == Direction.left
            ? new Vector2(-1f, -2f) : new Vector2(1f, -2f);
        vec.Normalize();

        while (true)
        {
            if (col.GetComponent<JumpSkillCollider>().isTriggered)
                break;

            else if (PlayManager.Instance.Player.stopJumpSkill)
                break;

            PlayManager.Instance.Player.JumpAttackMove(vec);

            yield return null;
        }

        col.GetComponent<DamagingCollider>().OnDestroyCallBack();
        PlayManager.Instance.Player.JumpAgain();

        onAttack = false;
        PlayManager.Instance.Player.IsMovable = true;
    }

    /*
    void OnTriggerStay2D(Collider2D col)   //임시로 만든 거
    {

        if (onAttack == true && col.transform.parent.tag == "Enemy" && col.transform.parent.GetComponent<Character>().IsSuper <= 0f)
        {
            Debug.Log("attack!");

            col.transform.parent.GetComponent<Character>().GetDamage(atk);

        }
    }
    */
}
