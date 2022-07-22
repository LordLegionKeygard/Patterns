/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
namespace RayWenderlich.CommandPatternInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SceneManager : MonoBehaviour
    {
        public const float CommandPauseTime = 0.5f;

        [Header("Set In Inspector")]
        [SerializeField]
        private Bot bot = null;
        [SerializeField]
        private UIManager uiManager = null;
        private List<BotCommand> botCommands = new List<BotCommand>();
        private Stack<BotCommand> undoStack = new Stack<BotCommand>();
        private Coroutine executeRoutine;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ExecuteCommands();
            }
            else if (Input.GetKeyDown(KeyCode.U)) //1
            {
                UndoCommandEntry();
            }
            else if (Input.GetKeyDown(KeyCode.R)) //2
            {
                RedoCommandEntry();
            }
            else
            {
                CheckForBotCommands();
            }
        }

        private void CheckForBotCommands()
        {
            var botCommand = BotInputHandler.HandleInput();
            if (botCommand != null && executeRoutine == null)
            {
                AddNewCommand(botCommand);
            }
            undoStack.Clear();
        }
        private void AddNewCommand(BotCommand botCommand)
        {
            undoStack.Clear();
            AddToCommands(botCommand);
        }

        private void AddToCommands(BotCommand botCommand)
        {
            botCommands.Add(botCommand);

            uiManager.InsertNewText(botCommand.ToString());
        }

        private void ExecuteCommands()
        {
            if (executeRoutine != null)
            {
                return;
            }

            executeRoutine = StartCoroutine(ExecuteCommandsRoutine());
        }

        private IEnumerator ExecuteCommandsRoutine()
        {
            Debug.Log("Executing...");

            uiManager.ResetScrollToTop();

            for (int i = 0, count = botCommands.Count; i < count; i++)
            {
                var command = botCommands[i];
                command.Execute(bot);

                uiManager.RemoveFirstTextLine();
                yield return new WaitForSeconds(CommandPauseTime);
            }

            botCommands.Clear();

            bot.ResetToLastCheckpoint();

            executeRoutine = null;
        }

        private void UndoCommandEntry()
        {
            if (executeRoutine != null || botCommands.Count == 0)
            {
                return;
            }

            undoStack.Push(botCommands[botCommands.Count - 1]);
            botCommands.RemoveAt(botCommands.Count - 1);

            uiManager.RemoveLastTextLine();
        }

        private void RedoCommandEntry()
        {
            if (undoStack.Count == 0)
            {
                return;
            }

            var botCommand = undoStack.Pop();
            AddToCommands(botCommand);
        }

    }
}
