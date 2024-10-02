using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
   public float scaleSpeed = 0.5f; // Tốc độ giảm scale
    public float minScale = 0.1f;   // Giới hạn nhỏ nhất của scale

    void Update()
    {
        // Lấy scale hiện tại của GameObject
        Vector3 currentScale = transform.localScale;

        // Giảm scale của trục X và Z theo thời gian
        if (currentScale.x > minScale && currentScale.z > minScale)
        {
            float newScaleX = currentScale.x - scaleSpeed * Time.deltaTime;
            float newScaleZ = currentScale.z - scaleSpeed * Time.deltaTime;

            // Đảm bảo scale không nhỏ hơn minScale
            newScaleX = Mathf.Max(newScaleX, minScale);
            newScaleZ = Mathf.Max(newScaleZ, minScale);

            // Áp dụng scale mới cho trục X và Z, giữ nguyên trục Y
            transform.localScale = new Vector3(newScaleX, currentScale.y, newScaleZ);
        }
    }
}
