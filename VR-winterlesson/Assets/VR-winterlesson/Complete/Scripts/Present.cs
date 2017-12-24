using System.Collections;
using UnityEngine;

namespace VRStudies { namespace WinterLesson {
			
	public class Present : MonoBehaviour {

		void OnCollisionEnter(Collision collision) {

			//プレイヤーと衝突したとき
			if( collision.gameObject.name == "Player" ){

				// ランダムな方向へ爆発させる
				var present = this.gameObject.GetComponent<Rigidbody>();
				present.AddForceAtPosition ( 
					new Vector3( Random.Range( -300, 300 ), 600, Random.Range( -300, 300 ) ), 
					new Vector3( Random.Range( -5, 5 ), Random.Range( -5, 5 ), Random.Range( -5, 5 ) ) 
				);

				// プレイヤーにも力を加える
				var player = collision.rigidbody;
				player.AddForce( 0, 30f, 0, ForceMode.Impulse );

				// 追加でプレゼントを生成する
				transform.parent.GetComponent<PresentMaker>().GeneratePresents( 30 );
			}
		}
	}
}}
