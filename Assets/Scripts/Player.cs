using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 7f;
	[SerializeField] private GameInput _gameInput;
	
	private bool _isWalking;
	
    private void Update()
    {
		Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
		
		Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y);
		
		float moveDistance = _moveSpeed * Time.deltaTime;
		float playerRadius = .7f;
		float playerHeight = 2f;
		
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

		if (!canMove)
		{
			//Cannot move towards moveDir

			//Attempt only X movement
			Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) 
			{
				//Can move only on the X
				moveDir = moveDirX;
            }
            else
            {
				//Cannot move only on the X

				//Attempt only Z movement
				Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) 
				{
					//Can move only on the Z
					moveDir = moveDirZ;
                }
                else 
				{
					//Cannot move in any direction
				}

			}

		}
		if (canMove)
		{
			//transform.position += moveDir * _moveSpeed * Time.deltaTime;	
			transform.position += moveDir * moveDistance;
		}
		
		_isWalking = moveDir != Vector3.zero;
		float rotationSpeed = 10f;
		transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
	
	public bool IsWalking()
	{
		return _isWalking;
	}
}
