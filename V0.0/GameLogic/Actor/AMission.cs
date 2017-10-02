using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public class AMission {
		public string name = "等待任务";
		public int downcount = 0;

		public AMission (string n, int d) {
			name = n;
			downcount = d;
		}

		public AMission () {
			name = "等待任务";
			downcount = 0;
		}

		public void Await () {
			if (downcount <= 0) {
				name = "等待任务";
				downcount = 0;
			}
		}

		public void Countdown () {
			if (downcount <= 0) {
				name = "等待任务";
				downcount = 0;
			} else {
				downcount--;
			}
		}
	}
}