/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using UniRx;
using System.Collections;

public enum E_GameState
{
	Title = 1,
	LevelSelect = 2,
	InGame_Ready = 3,
	InGame_Droping = 4,
	InGame_Clear = 5
}

public static class StateManager 
{
	private static ReactiveProperty<E_GameState> _nowGameState = new ReactiveProperty<E_GameState>();

	public static IReadOnlyReactiveProperty<E_GameState> nowGameStateProperty
    {
        get { return _nowGameState; }
    }

	public static void SetGameState(E_GameState gameState)
    {
		_nowGameState.Value = gameState;
    }
}