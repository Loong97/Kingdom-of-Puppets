using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class ActionLogic
	{
		public static ACharacter selChr;

		public static void Pick(RaycastHit hit)
		{
			ClearSelection();
			RenewSelection(hit.collider.gameObject);
			selChr.Await();
			//mainCanvas.SendMessage("ReadCharacter", ActionLogic.selChr.id);
		}

		public static void ClearSelection()
		{
			selChr = null;
			//mainCanvas.SendMessage("ClearActive");
		}

		public static void RenewSelection(GameObject s)
		{
			selChr = CharacterLogic.Find(s.name);
		}
	}
}