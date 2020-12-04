using System;
using System.Collections;

namespace protocoloManchester
{
    class Program
    {
        class CCelula
        {
            public Object Item; // O Item armazendo pela células
            public CCelula Prox; // Referencia a próxima célula

            public CCelula()
            {
                Item = null;
                Prox = null;
            }

            public CCelula(object ValorItem)
            {
                Item = ValorItem;
                Prox = null;
            }

            public CCelula(object ValorItem, CCelula ProxCelula)
            {
                Item = ValorItem;
                Prox = ProxCelula;
            }
        }

        class CFila
        {
            private CCelula Frente; // Referencia a primeira célula da CFila (Célula cabeça)
            private CCelula Tras; // Referencia a última célula da CFila
            private int Qtde = 0;

            public CFila()
            {
                Frente = new CCelula();
                Frente.Prox = new CCelula();
                Tras = Frente;
            }

            public bool Vazia()
            {
                return Frente == Tras;
            }

            public void Enfileira(Paciente item) // Defina os parâmetros que você achar necessários
            {
                if (Qtde == 0)
                {
                    insertAt(1, new CCelula(item));
                }
                else
                {
                    int prioridade = pegaPosicaoPrioridade(item.pulseira);
                    insertAt(prioridade, new CCelula(item));
                }
                Tras = GetTras();
                Qtde++;
            }

            private int pegaPosicaoPrioridade(Pulseira pulseira)
            {
                CCelula aux = Frente.Prox;
                int prioridade = 1;
                while (aux.Item != null)
                {
                    Paciente paciente = (Paciente)aux.Item;
                    if (pulseira >= paciente.pulseira)
                    {
                        prioridade++;
                    }
                    aux = aux.Prox;
                }

                return prioridade;
            }

            public Object Desenfileira()
            {
                Object Item = null;
                if (Frente != Tras)
                {
                    Frente = Frente.Prox;
                    Item = Frente.Item;
                    Qtde--;
                }
                return Item;
            }

            public Object Peek()
            {
                return (Frente != Tras) ? Frente.Prox.Item : null;
            }

            public bool Contem(Object elemento)
            {
                bool achou = false;
                for (CCelula aux = Frente.Prox; aux != null && !achou; aux = aux.Prox)
                    achou = aux.Item.Equals(elemento);
                return achou;
            }

            public int Quantidade()
            {
                return Qtde;
            }

            private void insertAt(int i, CCelula celula)
            {
                CCelula atual = Frente.Prox;

                while (i-- != 0)
                {
                    if (i == 0)
                    {
                        CCelula temporaria = new CCelula();
                        temporaria.Item = atual.Item;
                        temporaria.Prox = atual.Prox;
                        celula.Prox = temporaria;
                        atual.Item = celula.Item;
                        atual.Prox = celula.Prox;

                    }
                    else
                    {
                        atual = atual.Prox;
                    }
                }
            }


            public CCelula GetTras()
            {
                CCelula aux = Frente.Prox;
                while (aux != null)
                {
                    if (aux.Prox.Prox == null)
                        return aux;

                    aux = aux.Prox;
                }

                return null;
            }

            public IEnumerator GetEnumerator()
            {
                for (CCelula aux = Frente.Prox; aux != null; aux = aux.Prox)
                    yield return aux.Item;
            }
        }

        enum Pulseira
        {
            Vermelha,
            Laranja,
            Amarelo,
            Verde,
            Azul
        }

        class Paciente
        {
            public string nome;

            public Pulseira pulseira;

            public Paciente(string nome, Pulseira pulseira)
            {
                this.nome = nome;
                this.pulseira = pulseira;
            }
        }

        static Pulseira getPulseira(string pulseira)
        {
            if (pulseira.CompareTo("AMARELO") == 0)
                return Pulseira.Amarelo;
            if (pulseira.CompareTo("VERMELHO") == 0)
                return Pulseira.Vermelha;
            if (pulseira.CompareTo("VERDE") == 0)
                return Pulseira.Verde;
            if (pulseira.CompareTo("LARANJA") == 0)
                return Pulseira.Laranja;

            return Pulseira.Azul;
        }

        static void Main(string[] args)
        {
            CFila fila = new CFila();

            bool read = true;
            while (read)
            {

                string nome = Console.ReadLine();
                string pulseiraR = Console.ReadLine();

                if (nome.Length == 0) read = false;

                if (read)
                {
                    Pulseira pulseira = getPulseira(pulseiraR);
                    fila.Enfileira(new Paciente(nome, pulseira));
                }
            }

            Paciente paciente = null;
            int contador = 1;
            do
            {
                paciente = (Paciente)fila.Desenfileira();
                if (paciente != null)
                    Console.WriteLine(contador + " - " + paciente.nome);
                contador++;
            } while (paciente != null);

        }
    }
}