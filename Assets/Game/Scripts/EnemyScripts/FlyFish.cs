using EnemyScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFish : MonoBehaviour
{
    [Header("飞行参数")]
    [SerializeField] private float flightSpeed = 5f; // 水平飞行速度
    public float maxHeight = 3f;   // 最高点高度
    [SerializeField] private float horizontalDistance = 10f; // 水平飞行总距离
    [SerializeField] private AnimationCurve verticalCurve; // 垂直高度曲线

    [Header("调试")]
    [SerializeField] private bool drawTrajectory = true;
    [SerializeField] private Color trajectoryColor = Color.cyan;

    private Coroutine flightCoroutine;
    private Vector3 initialPosition;
    public EnemyController enemyController;

    void Start()
    {
        enemyController=GetComponent<EnemyController>();
    }

    /// <summary>
    /// 开始飞行
    /// </summary>
    public void StartFlight()
    {
        initialPosition = transform.position;
        if (flightCoroutine != null)
        {
            StopCoroutine(flightCoroutine);
        }
        flightCoroutine = StartCoroutine(FlightRoutine());
    }

    /// <summary>飞行协程 </summary>
    private IEnumerator FlightRoutine()
    {
        // 重置位置
        transform.position = initialPosition;

        // 计算飞行参数
        float flightDuration = horizontalDistance / flightSpeed;

        // 记录开始位置
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        // 方法1：使用二次函数抛物线
        while (elapsedTime < flightDuration)
        {
            // 计算水平位移
            float t = elapsedTime / flightDuration;
            float x = startPos.x - (t * horizontalDistance); // 向左移动

            // 使用抛物线方程：y = -a(x - 0.5)² + 1，再乘以maxHeight
            // 这个方程在t=0.5时达到最大值1
            float normalizedX = (t - 0.5f) * 2f; // 转换为-1到1的范围
            float parabola = -normalizedX * normalizedX + 1; // 抛物线形状
            float y = startPos.y + parabola * maxHeight;

            transform.position = new Vector3(x, y, startPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保到达终点
        Vector3 finalPosition = new Vector3(
            startPos.x - horizontalDistance,
            startPos.y,
            startPos.z
        );
        transform.position = finalPosition;
        enemyController.Die();
        Debug.Log("飞行完成");
    }

    /// <summary>
    /// 备用方法：使用正弦曲线（更平滑）
    /// </summary>
    private IEnumerator FlightRoutineSin()
    {
        // 重置位置
        transform.position = initialPosition;

        // 计算飞行参数
        float flightDuration = horizontalDistance / flightSpeed;

        // 记录开始位置
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        // 方法2：使用正弦函数，从0到π
        while (elapsedTime < flightDuration)
        {
            float t = elapsedTime / flightDuration;
            float x = startPos.x - (t * horizontalDistance);

            // 使用sin函数，从0到π，sin在0到π之间先上升后下降
            float sineValue = Mathf.Sin(t * Mathf.PI);
            float y = startPos.y + sineValue * maxHeight;

            transform.position = new Vector3(x, y, startPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(
            startPos.x - horizontalDistance,
            startPos.y,
            startPos.z
        );
        transform.position = finalPosition;
    }

    /// <summary>
    /// 使用AnimationCurve控制轨迹
    /// </summary>
    private IEnumerator FlightRoutineWithCurve()
    {
        // 重置位置
        transform.position = initialPosition;

        // 计算飞行参数
        float flightDuration = horizontalDistance / flightSpeed;

        // 如果没有设置曲线，使用默认的抛物线曲线
        if (verticalCurve.keys.Length == 0)
        {
            // 创建默认抛物线曲线
            verticalCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f);
            verticalCurve.AddKey(0.5f, 1f);
        }

        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < flightDuration)
        {
            float t = elapsedTime / flightDuration;
            float x = startPos.x - (t * horizontalDistance);

            // 使用AnimationCurve控制垂直位置
            float curveValue = verticalCurve.Evaluate(t);
            float y = startPos.y + curveValue * maxHeight;

            transform.position = new Vector3(x, y, startPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(
            startPos.x - horizontalDistance,
            startPos.y,
            startPos.z
        );
        transform.position = finalPosition;
    }

    /// <summary>
    /// 计算并绘制轨迹
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!drawTrajectory)
            return;

        Gizmos.color = trajectoryColor;

        int segments = 50;
        Vector3 startPos = Application.isPlaying ? initialPosition : transform.position;

        for (int i = 0; i < segments; i++)
        {
            float t1 = (float)i / segments;
            float t2 = (float)(i + 1) / segments;

            // 计算水平位置
            float x1 = startPos.x - (t1 * horizontalDistance);
            float x2 = startPos.x - (t2 * horizontalDistance);

            // 使用抛物线方程计算垂直位置
            float normalizedX1 = (t1 - 0.5f) * 2f;
            float parabola1 = -normalizedX1 * normalizedX1 + 1;
            float y1 = startPos.y + parabola1 * maxHeight;

            float normalizedX2 = (t2 - 0.5f) * 2f;
            float parabola2 = -normalizedX2 * normalizedX2 + 1;
            float y2 = startPos.y + parabola2 * maxHeight;

            Vector3 point1 = new Vector3(x1, y1, startPos.z);
            Vector3 point2 = new Vector3(x2, y2, startPos.z);

            Gizmos.DrawLine(point1, point2);
        }

        // 标记起点和终点
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPos, 0.3f);

        Vector3 endPos = new Vector3(
            startPos.x - horizontalDistance,
            startPos.y,
            startPos.z
        );
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPos, 0.3f);
    }

    /// <summary>
    /// 重新开始飞行
    /// </summary>
    public void RestartFlight()
    {
        StartFlight();
    }

    /// <summary>
    /// 停止飞行
    /// </summary>
    public void StopFlight()
    {
        if (flightCoroutine != null)
        {
            StopCoroutine(flightCoroutine);
            flightCoroutine = null;
        }
    }

    /// <summary>
    /// 设置飞行参数
    /// </summary>
    public void SetFlightParameters(float height)
    {
        maxHeight = height;
        StartFlight();
    }

    /// <summary>
    /// 获取飞行状态
    /// </summary>
    public bool IsFlying()
    {
        return flightCoroutine != null;
    }
}