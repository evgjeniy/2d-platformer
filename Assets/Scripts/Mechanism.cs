using DG.Tweening;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Collider2D))]
public class Mechanism : MonoCashed<Collider2D>
{
    [SerializeField] private Vector2 moveOffset = new (0.0f, 3.0f);
    [SerializeField] private float moveDuration = 3;
    [SerializeField] private Ease moveEase = Ease.InSine;

    public void Move()
    {
        transform.DOMove(position + (Vector3)moveOffset, moveDuration)
            .SetEase(moveEase).SetLink(gameObject).OnKill(First.Disable);
    }
}