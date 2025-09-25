using UnityEngine;

public class CardAnimator : MonoBehaviour
{
    public void MoveTo(Vector3 targetPosition, float duration = 0.5f)
    {
        // Двигаем карту в targetPosition за duration секунд
        LeanTween.move(gameObject, targetPosition, duration).setEaseOutCubic();
    }

    public void Flip(float duration = 0.5f)
    {
        // Переворот по оси Y
        LeanTween.rotateY(gameObject, 90, duration / 2).setOnComplete(() =>
        {
            LeanTween.rotateY(gameObject, 0, duration / 2);
        });
    }

    public void ScaleIn(float duration = 0.3f)
    {
        // Появление
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, duration).setEaseOutBack();
    }

    public void ScaleOut(float duration = 0.3f)
    {
        // Исчезновение
        LeanTween.scale(gameObject, Vector3.zero, duration).setEaseInBack();
    }
}