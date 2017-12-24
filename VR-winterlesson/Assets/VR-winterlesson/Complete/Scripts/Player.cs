using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRStudies { namespace WinterLesson {

	public class Player : MonoBehaviour {

		//------------------------------------------------------------------------------------------------------------------------------//
		void Update () {

			// y座標がマイナスになったらシーンをリセットする
			if( this.transform.position.y < -100 ){
				SceneManager.LoadScene (0);
			}

			// アバターをカメラと同じ方向へ向かせる
			var avatar = this.transform.Find("Avatar");
			var camera = GameObject.Find("Main Camera");
			avatar.transform.forward = camera.transform.forward;

			// 視線ポインターを非アクティブにする
			camera.transform.Find("Pointer/Pointer").gameObject.SetActive(false);

			// カメラ中央からプレイヤーの視線方向へレイキャスト
			Ray ray = new Ray( camera.transform.position, camera.transform.forward );
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 300.0f, 1)) {

				// プレゼントと視線が衝突したとき
				if (hit.collider.gameObject.name == "Present") {

					// 視線ポインターをアクティブにする
					camera.transform.Find("Pointer/Pointer").gameObject.SetActive(true);

					// Rayの衝突地点と現在のプレイヤーの位置から速度ベクトルを作成する
					var direction = hit.point - this.transform.position; 
					Vector3 velocity = direction * 1.5f;

					// プレイヤーに力を加えて移動させる
					var rigid = this.gameObject.GetComponent<Rigidbody> ();
					rigid.AddForce (velocity.x, 0, velocity.z, ForceMode.Force);
				}
			} 
		}


		//オブジェクトと衝突したとき
		void OnCollisionEnter(Collision collision) {

			// ジャンプが止まらないように常に上方向に力を加える
			var rigid = this.gameObject.GetComponent<Rigidbody>();
			rigid.AddForce( 0, 3f, 0, ForceMode.Impulse);

			// 一瞬だけ縦方向に縮める
			StartCoroutine( "animateHopping" );

			// プレゼントを踏んだ場合
			if (collision.gameObject.name == "Present") {
				
				// モーションアニメを再生する
				Animator animator = transform.Find ("Avatar/Santa").GetComponent<Animator> ();
				animator.Play ("Yeah", 0, 0.0f);
				
				// サンタの声を再生する
				AudioSource sound = this.GetComponent<AudioSource>();
				sound.Play ();
			}
		}

		// ホッピングの簡易アニメーション
		IEnumerator animateHopping() {

			// 縦方向のスケールを縮める
			transform.Find("Avatar").localScale = new Vector3 ( 1, 0.9f, 1 );

			//コルーチンで一定フレーム待つ
			yield return new WaitForSeconds (0.08f);

			// 縦方向のスケールを戻す
			transform.Find("Avatar").localScale = new Vector3 ( 1, 1, 1 );
		}
	}
}}
