using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵이동시 리스폰 되는 오브젝트를 의미 
/// </summary>
public interface Respawnable
{
    /// <summary>
    /// 리스폰(스테이지 리셋 시 사용)
    /// </summary>
    void Reset();

}
