using System.Collections;
using UnityEngine;

namespace VRStudies { namespace WinterLesson {

	public class PresentMaker : MonoBehaviour {


		void Start() {

			// 初回のプレゼントを生成する
			GeneratePresents ( 250 );
		}

		public void GeneratePresents( int num ) {

			//プレゼントを大量複製して空から降らす
			for( int i = 0; i < num; i++ ){

				//ランダムな初期位置へ移動
				var present = transform.Find("Present").gameObject;
				GameObject copy = Object.Instantiate( present ) as GameObject;
				copy.name = "Present";
				copy.transform.parent = this.transform;
				copy.transform.position = new Vector3( Random.Range( -150, 150 ), Random.Range( 30, 300 ), Random.Range( -150, 150 ) );
			}
		}
	}

}}
