using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIAnimator))]
public class TutorialPanel : MonoBehaviour {

	[SerializeField]
	private TextEffects title;

	[SerializeField]
	private TextEffects body;

	[SerializeField]
	private TextEffects buttonText;

	[SerializeField]
	private UIAnimator button;

	private UIAnimator uiAnimator;

	void Awake () {
		uiAnimator = GetComponent<UIAnimator> ();
	}

	public void DisplayStep1 () {		
		VerticalExpand ();
	}

	public void DisplayStep2 () {
		title.SetText ("");
		body.SetText ("");
		Step2Title (); 
	}

	public void DisplayStep3 () {
		Step3Instructions1 ();
	}

	public void DisplayStep5 () {
		title.SetText ("");
		body.SetText ("");
		Step5Title ();
	}

	public void DisplayStep6 () {
		Step6Instructions1 ();
	}

	public void DisplayStep9 () {
		title.SetText ("");
		body.SetText ("");
		Step9Title ();
	}

	public void DisplayStep10 (Action onComplete) {
		title.SetText ("");
		body.SetText ("");
		Step10Title (onComplete);
	}

	private void VerticalExpand () {
		uiAnimator.Resize (() => HorizontalExpand(), new Vector3 (0.5f, 0f), new Vector3 (0.5f, 1f), 0.5f, 0.5f);
	}

	private void HorizontalExpand () {
		uiAnimator.Resize (() => Step1Title(), new Vector3 (0.5f, 1f), new Vector3 (1f, 1f), 0.25f, 0.5f);
	}
		
	public void ShowTitle (string title, Action onComplete, float delay = 0f) {
		StartCoroutine(this.title.TypeText (title, onComplete, 0.75f, delay));	
	}

	public void ShowBody (string text, Action onComplete, float duration, float delay = 0f, bool append = false) {
		StartCoroutine(this.body.FadeInText (text, onComplete, duration, delay, append));	
	}

	public void ShowButton (string text, Action onComplete, float delay = 0f, float duration = 0.25f) {
		buttonText.SetText (text);
		button.Resize (onComplete, new Vector3 (0f, 1f), new Vector3 (1f, 1f), delay, duration);
	}

	public void HideButton (Action onComplete) {
		button.Resize (onComplete, new Vector3 (1f, 1f), new Vector3 (0f, 1f));
	}

	private void Step1Title()
	{
		ShowTitle ("CLAIMING", () => Step1Instructions1());
	}

	private void Step1Instructions1()
	{
		ShowBody ("In fortify players take turns claiming tiles on the hex board", () => Step1Instructions2(), 1f, 0.5f);
	}

	private void Step1Instructions2()
	{
		ShowBody ("\n\n\nOnce all tiles have been claimed the game ends and the player that owns the most tiles is the winner", () => Step1ButtonOk(), 1f, 1.5f, true);
	}

	private void Step1ButtonOk()
	{
		ShowButton ("OK", () => {}, 2.5f);
	}

	private void Step2Title()
	{
		ShowTitle ("CAPTURING", () => Step2Instructions1(), 0.25f);
	}

	private void Step2Instructions1()
	{
		ShowBody ("Now it's your opponents turn to claim a tile", () => Step2ButtonOk(), 1f, 0.5f);
	}

	private void Step2ButtonOk()
	{
		ShowButton ("OK", () => {}, 0.75f);
	}

	private void Step3Instructions1()
	{
		ShowBody ("\n\n\nThe opponent captured your tile! Whenever a player claims a tile they also capture all ordinary adjacent tiles of the other player", () => Step3ButtonOk(), 1f, 1f, true);
	}

	private void Step3ButtonOk()
	{
		ShowButton ("OK", () => {}, 2.5f);
	}

	private void Step5Title()
	{
		ShowTitle ("FORTIFYING", () => Step5Instructions1(), 0.5f);
	}

	private void Step5Instructions1()
	{
		ShowBody ("You can fortify tiles you own by claiming an adjacent tile", () => Step5ButtonOk(), 1f, 0.5f);
	}

	private void Step5ButtonOk()
	{
		ShowButton ("OK", () => {}, 1f);
	}

	private void Step6Instructions1()
	{
		ShowBody ("\n\n\nWhen a tile is fortified it will not be captured like normal but will have its fortification removed instead", () => Step6ButtonOk(), 1f, 1f, true);
	}

	private void Step6ButtonOk()
	{
		ShowButton ("OK", () => {}, 1.5f);
	}

	private void Step9Title()
	{
		ShowTitle ("SACRIFICING", () => Step9Instructions1(), 0.5f);
	}

	private void Step9Instructions1()
	{
		ShowBody ("On your turn you can choose to sacrifice a fortified tile instead of claiming a new one", () => Step9Instructions2(), 1f, 0.5f);
	}

	private void Step9Instructions2()
	{
		ShowBody ("\n\n\nSacrificing removes your tiles fortification in exchange for capturing all ordinary adjacent tiles of your opponent", () => Step9ButtonOk(), 1f, 1.5f, true);
	}

	private void Step9ButtonOk()
	{
		ShowButton ("OK", () => {}, 1.5f);
	}

	private void Step10Title(Action onComplete)
	{
		ShowTitle ("WELL DONE!", () => Step10Instructions1(onComplete), 0.5f);
	}

	private void Step10Instructions1(Action onComplete)
	{
		ShowBody ("Now you know all the rules of Fortify!", () => Step10Instructions2(onComplete), 1f, 0.5f);
	}

	private void Step10Instructions2(Action onComplete)
	{
		ShowBody ("\n\n\nFeel free to play on or click the finish button to go to the main menu", () => Step10ButtonFinish(onComplete), 1f, 0.75f, true);
	}

	private void Step10ButtonFinish(Action onComplete)
	{
		ShowButton ("FINISH", () => onComplete(), 1f);
	}



}
