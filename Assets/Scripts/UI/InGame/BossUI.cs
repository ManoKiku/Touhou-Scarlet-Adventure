using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public static BossUI instance;

    [Header("UI elements")]
    public Image bossHP;
    public Animator spellCard;
    public CinemachineVirtualCamera vc;
    public CinemachineConfiner cc;
    public Collider2D cameraBounds;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        if(BossStatus.instance == null)
        {
            return;
        }

        if(BossStatus.instance.bossStages.Count != 0)
        {
            BossStage buff = BossStatus.instance.bossStages.First();
            bossHP.fillAmount = (float)buff.hpAmount / buff.maxHP;
        }
    }
}
