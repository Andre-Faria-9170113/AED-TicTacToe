using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_2
{
    class Program
    {
        static char[,] arrayTabuleiro = new char[3, 3] { {'-','-','-'}, { '-', '-', '-' }, { '-', '-', '-' } };
        static string PlayerOne = "";
        static string PlayerTwo = "COMPUTER";
        static int countPlays = 0;
        static char playSymbol = 'O';
        static string winningPlayer = "";

        //MÉTODO MENU
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("**********   JOGO DO GALO    **********");
            Console.WriteLine("         1. SINGLE-PLAYER");
            Console.WriteLine("         2. TWO-PLAYER");
            Console.WriteLine("         0. QUIT");
            Console.WriteLine("***************************************");
            Console.Write("\nOPÇÃO: ");
            ConsoleKeyInfo inputKey = Console.ReadKey();
            switch (inputKey.Key)
            {
                
                case ConsoleKey.D0:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.D1:
                    SinglePlayer();
                    break;
                case ConsoleKey.D2:
                    MultiPlayer();
                    break;
                case ConsoleKey.NumPad0:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.NumPad1:
                    SinglePlayer();
                    break;
                case ConsoleKey.NumPad2:
                    MultiPlayer();
                    break;
                default:
                    Console.WriteLine("OPÇÃO INVÁLIDA! Escolhade novo.");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;
                    
            }


        }

        //MÉTODO PRINT TABULEIRO
        static void PrintTabuleiro()
        {
            if(playSymbol == 'X')
            {
                Console.WriteLine("\n              " + PlayerOne + " (JOGADOR 1)");
            }

            else if (playSymbol == 'O')
            {
                Console.WriteLine("\n              " + PlayerTwo + " (JOGADOR 2)");
            }

            for (int i = 0; i < 3; i++)
            {
                string linha = "";
                for (int j = 0; j < 3; j++)
                {
                    linha += "       "+arrayTabuleiro[i,j] + "       ";
                }
                Console.WriteLine("\n\n\n\n"+linha);
            }
        }

        //MÉTODO INPUT JOGADA PLAYER
        static void InputJogadaPlayer()
        {
            if (countPlays % 2 == 0)
            {
                playSymbol = 'X';
            }
            else
            {
                playSymbol = 'O';
            }

            inputCasa:
            Console.Clear();
            PrintTabuleiro();

            int indexLinha;
            int indexColuna;

            ConsoleKeyInfo inputKey = Console.ReadKey();
            switch (inputKey.Key)  
            {
                
                case ConsoleKey.NumPad1:
                    indexLinha = 2;
                    indexColuna = 0;
                    break;
                case ConsoleKey.NumPad2:
                    indexLinha = 2;
                    indexColuna = 1;
                    break;
                case ConsoleKey.NumPad3:
                    indexLinha = 2;
                    indexColuna = 2;
                    break;
                case ConsoleKey.NumPad4:
                    indexLinha = 1;
                    indexColuna = 0;
                    break;
                case ConsoleKey.NumPad5:
                    indexLinha = 1;
                    indexColuna = 1;
                    break;
                case ConsoleKey.NumPad6:
                    indexLinha = 1;
                    indexColuna = 2;
                    break;
                case ConsoleKey.NumPad7:
                    indexLinha = 0;
                    indexColuna = 0;
                    break;
                case ConsoleKey.NumPad8:
                    indexLinha = 0;
                    indexColuna = 1;
                    break;
                case ConsoleKey.NumPad9:
                    indexLinha = 0;
                    indexColuna = 2;
                    break;
                default:
                    Console.WriteLine("ESCOLHA DE JOGADA INVÁLIDA!");
                    Console.ReadLine();
                    goto inputCasa;
            }


            //VERIFICAR SE POSIÇÃO JÁ ESTÁ OCUPADA
            if(arrayTabuleiro[indexLinha,indexColuna] != '-')
            {
                Console.WriteLine("ERRO! A posição indicada já se encontra ocupada.");
                Console.ReadLine();
                goto inputCasa;
            }

            //OCUPAR POSIÇÃO
            arrayTabuleiro[indexLinha, indexColuna] = playSymbol;
            countPlays++;

            //DETERMINAR SE HOUVE VITÓRIA
            if(checkVictory() != 0)
            {
                endMessage:
                Console.Clear();
                PrintTabuleiro();
                Console.WriteLine("\n\n              VITÓRIA DE " + winningPlayer+" (JOGADOR "+checkVictory()+")!!!\n\n(Prima 'R' para desforra, 'M' para regressar ao Menu Inicial, ou 'ESC' para sair do jogo)");
                ConsoleKeyInfo inputOption = Console.ReadKey();
                switch (inputOption.Key)
                {
                    
                    case ConsoleKey.M:
                        Menu();
                        break;
                    case ConsoleKey.R:
                        Console.Clear();
                        winningPlayer = "";
                        countPlays = 0;
                        arrayTabuleiro = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
                        PrintTabuleiro();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("OPÇÃO INVÁLIDA! Escolha de novo.");
                        Console.ReadKey();
                        goto endMessage;
                        
                }
            }
        }
        //MÉTODO INPUT JOGADA DO COMPUTADOR
        static void InputJogadaCPU()
        {
            if (countPlays % 2 == 0)
            {
                playSymbol = 'X';
            }
            else
            {
                playSymbol = 'O';
            }

            int inputLinha = 3;
            int inputColuna = 3;


            //HIERARQUIA DE DECISÕES
            //DETERMINAR CASA A ESCOLHER
            //VERIFICAR SE PODE GANHAR
            int countO = 0;

            //LINHAS
            for (int i = 0; i < 3; i++)
            {
                countO = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arrayTabuleiro[i, j] == 'O')
                    {
                        countO++;
                    }
                }

                if (countO == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (arrayTabuleiro[i, j] == '-')
                        {
                            inputLinha = i;
                            inputColuna = j;
                        }
                    }
                }
            }

            //COLUNAS
            for (int i = 0; i < 3; i++)
            {
                countO = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arrayTabuleiro[j, i] == 'O')
                    {
                        countO++;
                    }
                }

                if (countO == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (arrayTabuleiro[j, i] == '-')
                        {
                            inputLinha = j;
                            inputColuna = i;
                        }
                    }
                }
            }

            //1ª DIAGONAL
            countO = 0;
            for (int i = 0; i < 3; i++)
            {
                if (arrayTabuleiro[i, i] == 'O')
                {
                    countO++;
                }

                if (countO == 2)
                {
                    if (arrayTabuleiro[i, i] == '-')
                    {
                        inputLinha = i;
                        inputColuna = i;
                    }
                }
            }

            //2ª DIAGONAL
            countO = 0;
            for (int i = 2; i >= 0; i--)
            {
                if (arrayTabuleiro[2-i, i] == 'O')
                {
                    countO++;
                }

                if (countO == 2)
                {
                    for (int j = 2; j >= 0; j--)
                    {
                        if (arrayTabuleiro[2-j, j] == '-')
                        {
                            inputLinha = 2-j;
                            inputColuna = j;
                        }
                    }
                    
                }
            }

            //VERIFICAR SE ESTÁ EM RISCO DE PERDER
            if (inputLinha == 3 || inputColuna == 3)
            {
                int countX = 0;

                //LINHAS
                for (int i = 0; i < 3; i++)
                {
                    countX = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (arrayTabuleiro[i, j] == 'X')
                        {
                            countX++;
                        }
                    }

                    if (countX == 2)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (arrayTabuleiro[i, j] == '-')
                            {
                                inputLinha = i;
                                inputColuna = j;
                            }
                        }
                    }
                }

                //COLUNAS
                for (int i = 0; i < 3; i++)
                {
                    countX = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (arrayTabuleiro[j, i] == 'X')
                        {
                            countX++;
                        }
                    }

                    if (countX == 2)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (arrayTabuleiro[j, i] == '-')
                            {
                                inputLinha = j;
                                inputColuna = i;
                            }
                        }
                    }
                }

                //1ª DIAGONAL
                countX = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (arrayTabuleiro[i, i] == 'X')
                    {
                        countX++;
                    }

                    if (countX == 2)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (arrayTabuleiro[j, j] == '-')
                            {
                                inputLinha = j;
                                inputColuna = j;
                            }
                        }
                        
                    }
                }

                //2ª DIAGONAL
                countX = 0;
                for (int i = 2; i >= 0; i--)
                {
                    if (arrayTabuleiro[2-i, i] == 'X')
                    {
                        countX++;
                    }

                    if (countX == 2)
                    {
                        for (int j = 2; j >= 0; j--)
                        {
                            if (arrayTabuleiro[2 - j, j] == '-')
                            {
                                inputLinha = 2-j;
                                inputColuna = j;
                            }
                        }
                        
                    }
                }

                if (inputLinha == 3 || inputColuna == 3)
                {
                    //ESCOLHER CASA LIVRE ALEATÓRIA
                    Random rnd = new Random();
                    inputLinha = rnd.Next(0, 2);
                    inputColuna = rnd.Next(0, 2);
                    while (arrayTabuleiro[inputLinha, inputColuna] != '-')
                    {
                        inputLinha = rnd.Next(0, 3);
                        inputColuna = rnd.Next(0, 3);

                        if (arrayTabuleiro[inputLinha, inputColuna] == '-')
                        {
                            break;
                        }
                    }
                }
            }
           
               


            

            //OCUPAR CASA
            arrayTabuleiro[inputLinha, inputColuna] = playSymbol;
            countPlays++;

            //VERIFICAR VITORIA DO COMPUTADOR
            if(checkVictoryCPU() == 1)
            {
                endMessage:
                Console.Clear();
                PrintTabuleiro();
                Console.WriteLine("\n\n              GAME OVER!!!\n\n(Prima 'R' para desforra, 'M' para regressar ao Menu Inicial, ou 'ESC' para sair do jogo)");
                ConsoleKeyInfo inputOption = Console.ReadKey();
                switch (inputOption.Key)
                {

                    case ConsoleKey.M:
                        Menu();
                        break;
                    case ConsoleKey.R:
                        Console.Clear();
                        winningPlayer = "";
                        countPlays = 0;
                        arrayTabuleiro = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
                        PrintTabuleiro();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("OPÇÃO INVÁLIDA! Escolha de novo.");
                        Console.ReadKey();
                        goto endMessage;

                }
            }
        }

        //MÉTODO VERIFICAR VITÓRIA DO CPU
        static int checkVictoryCPU()
        {
            int winState = 0;
            int countO = 0;

            //LINHAS
            for (int i = 0; i < 3; i++)
            {
                countO = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arrayTabuleiro[i, j] == 'O')
                    {
                        countO++;
                    }

                    if (countO == 3)
                    {
                        winState = 1;
                        winningPlayer = "CPU";
                    }
                }
            }
            //COLUNAS
            for (int i = 0; i < 3; i++)
            {
                countO = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arrayTabuleiro[j, i] == 'O')
                    {
                        countO++;
                    }

                    if (countO == 3)
                    {
                        winState = 1;
                        winningPlayer = "CPU";
                    }
                }
            }

            //1ª DIAGONAL
            countO = 0;
            for (int i = 0; i < 3; i++)
            {
                if (arrayTabuleiro[i, i] == 'O')
                {
                    countO++;
                }

                if (countO == 3)
                {
                    winState = 1;
                    winningPlayer = "CPU";
                }

            }

            //2ª DIAGONAL
            countO = 0;

            for (int i = 0; i < 3; i++)
            {
                if (arrayTabuleiro[i, 2 - i] == 'O')
                {
                    countO++;
                }

                if (countO == 3)
                {
                    winState = 1;
                    winningPlayer = "CPU";
                }
            }

            //RETURN
           return winState;
        }

        //MÉTODO VERIFICAR VITÓRIA
        static int checkVictory()
        {
            int winState = 0;
            int countX = 0;
            int countO = 0;

            //LINHAS
            for (int i = 0; i < 3; i++)
            {
                countX = 0;
                countO = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arrayTabuleiro[i, j] == 'X')
                    {
                        countX++;
                    }
                    if (arrayTabuleiro[i, j] == 'O')
                    {
                        countO++;
                    }
                    if (countX == 3)
                    {
                        winState = 1;
                        winningPlayer = PlayerOne;
                    }
                    else if (countO == 3)
                    {
                        winState = 2;
                        winningPlayer = PlayerTwo;
                    }
                }
            }
            //COLUNAS
            for (int i = 0; i < 3; i++)
            {
                countX = 0;
                countO = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (arrayTabuleiro[j, i] == 'X')
                    {
                        countX++;
                    }
                    if (arrayTabuleiro[j, i] == 'O')
                    {
                        countO++;
                    }
                    if (countX == 3)
                    {
                        winState = 1;
                        winningPlayer = PlayerOne;
                    }
                    else if (countO == 3)
                    {
                        winState = 2;
                        winningPlayer = PlayerTwo;
                    }
                }
            }

            //1ª DIAGONAL
            countX = 0;
            countO = 0;
            for (int i = 0; i < 3; i++)
            {
                    if (arrayTabuleiro[i, i] == 'X')
                    {
                        countX++;
                    }
                    if (arrayTabuleiro[i, i] == 'O')
                    {
                        countO++;
                    }

                if (countX == 3)
                {
                    winState = 1;
                    winningPlayer = PlayerOne;
                }
                else if (countO == 3)
                {
                    winState = 2;
                    winningPlayer = PlayerTwo;
                }

            }

            //2ª DIAGONAL
            countX = 0;
            countO = 0;

            for (int i = 0; i < 3; i++)
            {
                if (arrayTabuleiro[i, 2-i] == 'X')
                {
                    countX++;
                }
                if (arrayTabuleiro[i, 2-i] == 'O')
                {
                    countO++;
                }

                if (countX == 3)
                {
                    winState = 1;
                    winningPlayer = PlayerOne;
                }
                else if (countO == 3)
                {
                    winState = 2;
                    winningPlayer = PlayerTwo;
                }
            }

            //RETURN
            return winState;
        }
        

        //MÉTODO 1PLAYER
        static void SinglePlayer()
        {
            Console.Clear();
           
            //RESET AO TABULEIRO
            arrayTabuleiro = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };

            //RESET AO NUMERO DE JOGADAS
            countPlays = 0;


            //INPUT NOME P1
            Console.Write("P1: ");
            PlayerOne = Console.ReadLine();

            //NOME P2
            PlayerTwo = "COMPUTER";

            PrintTabuleiro();

            singlePlayerLoop:
            while (countPlays < 9)
            {
                if(countPlays % 2 == 0)
                {
                    InputJogadaPlayer();
                }
                else
                {
                    InputJogadaCPU();
                }
            }

            endMessage:
            Console.Clear();
            PrintTabuleiro();
            Console.WriteLine("\n\n              EMPATE!!!\n\n(Prima 'R' para desforra, 'M' para regressar ao Menu Inicial, ou 'ESC' para sair do jogo)");
            ConsoleKeyInfo inputOption = Console.ReadKey();
            switch (inputOption.Key)
            {

                case ConsoleKey.M:
                    Menu();
                    break;
                case ConsoleKey.R:
                    Console.Clear();
                    winningPlayer = "";
                    countPlays = 0;
                    arrayTabuleiro = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
                    PrintTabuleiro();
                    goto singlePlayerLoop;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("OPÇÃO INVÁLIDA! Escolha de novo.");
                    Console.ReadKey();
                    goto endMessage;

            }

        }

        //MÉTODO 2PLAYER
        static void MultiPlayer()
        {
            Console.Clear();
            //RESET AO TABULEIRO
            arrayTabuleiro = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };

            //RESET AO NUMERO DE JOGADAS
            countPlays = 0;

            //INPUT NOME P2
            Console.Write("P1: ");
            PlayerOne = Console.ReadLine();

            //INPUT NOME P2
            Console.Write("P2: ");
            PlayerTwo = Console.ReadLine();

            PrintTabuleiro();
            multiplayerLoop:
            while (countPlays < 9)
            {
                InputJogadaPlayer();
            }

            endMessage:
            Console.Clear();
            PrintTabuleiro();
            Console.WriteLine("\n\n              EMPATE!!!\n\n(Prima 'R' para desforra, 'M' para regressar ao Menu Inicial, ou 'ESC' para sair do jogo)");
            ConsoleKeyInfo inputOption = Console.ReadKey();
            switch (inputOption.Key)
            {

                case ConsoleKey.M:
                    Menu();
                    break;
                case ConsoleKey.R:
                    Console.Clear();
                    winningPlayer = "";
                    countPlays = 0;
                    arrayTabuleiro = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
                    PrintTabuleiro();
                    goto multiplayerLoop;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("OPÇÃO INVÁLIDA! Escolha de novo.");
                    Console.ReadKey();
                    goto endMessage;

            }

        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
