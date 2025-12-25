using EnemyScripts;
using System.Collections;
using UnityEngine;

public class FlyKoopa : MonoBehaviour
{
    [Header("飞行设置")]
    public float waitTime = 2f;  // 跳跃间隔时间
    public float jumpHeight = 3f;  // 跳跃高度
    public float jumpDuration = 1f;  // 单次跳跃持续时间（上升+下降）
    public AnimationCurve jumpCurve;  // 跳跃曲线

    [Header("状态")]
    public bool canFly = true;
    public bool isJumping = false;


    public float originalY;
    private Coroutine jumpCoroutine;

    public void OnStartFly()
    {
        // 默认跳跃曲线：上快下慢
        if (jumpCurve == null || jumpCurve.keys.Length == 0)
        {
            jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }

        StartCoroutine(FlyJumpRoutine());
    }

    IEnumerator FlyJumpRoutine()
    {
        while (canFly)
        {
            if (canFly && !isJumping)
            {
                StartCoroutine(ParabolicJump());
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator ParabolicJump()
    {
        isJumping = true;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, originalY + jumpHeight, startPos.z);

        // 上升阶段
        while (elapsedTime < jumpDuration / 2f && canFly)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (jumpDuration / 2f);
            float curveValue = jumpCurve.Evaluate(t);

            // 计算当前位置
            float newY = Mathf.Lerp(startPos.y, targetPos.y, curveValue);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null;
        }

        // 短暂停留在顶部
        yield return new WaitForSeconds(0.1f);

        // 下降阶段
        startPos = transform.position;
        targetPos = new Vector3(transform.position.x, originalY, transform.position.z);
        elapsedTime = 0f;

        while (elapsedTime < jumpDuration / 2f && canFly)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (jumpDuration / 2f);
            float curveValue = jumpCurve.Evaluate(t);

            // 计算当前位置
            float newY = Mathf.Lerp(startPos.y, targetPos.y, curveValue);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null;
        }

        // 确保回到精确位置
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        isJumping = false;

    }

    // 开始飞行
    public void StartFlying()
    {
        canFly = true;
        if (!isJumping)
        {
            StartCoroutine(FlyJumpRoutine());
        }
    }

    // 停止飞行
    public void StopFlying()
    {
        canFly = false;
        isJumping = false;

        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
        }

        // 回到起始高度
        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
    }

    // 在Scene视图中显示辅助线
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Vector3 originalPos = new Vector3(transform.position.x, originalY, transform.position.z);
            Vector3 maxPos = new Vector3(transform.position.x, originalY + jumpHeight, transform.position.z);

            Gizmos.DrawWireSphere(originalPos, 0.3f);
            Gizmos.DrawWireSphere(maxPos, 0.3f);
            Gizmos.DrawLine(originalPos, maxPos);
        }
    }
}