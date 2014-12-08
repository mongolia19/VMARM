using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VMARM
{
    public class CPU
    {
        const int PC = 15;
        const int LR = 14;
        const int SP = 13;
        public UInt32 [] R =new UInt32[64];//The regs unsigned 32 bit regs
        //SP ->R13 ;PC ->R15 LR ->R14
        public UInt32[] RAM = new UInt32[200];//The ram of cpu
        
        
        Int64 op1;
        Int64 op2;
        Int64 op3;

        
       public CPUState CurrentState = CPUState.fetch;
       public static CPUState StateSwitcher(CPUState CS) 
        {
            if (CPUState.fetch== CS)
            {
                return CPUState.dec;
            }
            else if (CPUState.dec == CS)
            {
                return CPUState.getFromReg;

            }
            else if (CPUState.getFromReg==CS)
            {
                return CPUState.CALinALU;
            }

            else if (CPUState.CALinALU==CS)
            {
                return CPUState.AccessMem;
            }
            else if (CPUState.AccessMem==CS)
            {
                return CPUState.AccessReg;
            }
            else  //(CPUState.AccessReg==CS)
            {
                return CPUState.fetch;
            }
        
        
        }
       public enum CPUState 
        {
            HLT,
            fetch,
            dec,
            getFromReg,
            CALinALU,
            AccessMem,
            AccessReg
        }
        /// <summary>
        /// Every cmd takes up four Uint32s
        /// OpCode op0 op1 op2
        /// </summary>
       // enum CMD : int
        //{ 
       public const int     MOVMR=1;//move from mem to reg   mov reg []
       public const int     MOVRR=2;
       public const int     ADDRR=3;
       public const int     SUBRR=4;
       public const int     MULTRR=5;
       public const int     AND=6;
       public const int     OR=7;
       public const int     XOR=8;//x or
       public const int     LSL=9;//logical shift right
       public const int     LSR=10;//
       public const int     ASR=11;//Arthmetic Shift Right
       public const int     ROR=12;//(Rotate right)
       public const int     RRX=13;//Rotate Right eXtended by 1 place
       public const int     LDR=14;
       public const int     STR=15;//store to mem
       public const int     B=16;//branch
       public const int     BEQ=17;//Branch if equal
       public const int     BL=18;//branch with link store copy next cmd address to Link register(P14)
       public const int     TEQ=19;//test equal or not if equal, branch to the last oprand :TEQ op1 op2 [op3]
       public const int     TZ =20;//test if equal to zero if so branch to the second op :TZ op1 [op2]

       // }
        public static void  OneTick(CPU cpu)
        {
            CPUState CS=cpu.CurrentState; 
       
            if (CPUState.fetch== CS)
            {
                CPU.fetch(cpu);
            }
            else if (CPUState.dec == CS)
            {
                
                CPU.decode(cpu);
            }
            else if(CPUState.getFromReg==CS)
            {
                CPU.GetFromReg(cpu);
            }
            else if (CPUState.CALinALU==CS)
            {
               CPU.Calculate(cpu);
            }
            else if (CPUState.AccessMem==CS)
            {
                CPU.AccessMem(cpu);
            }
            else  //(CPUState.AccessReg==CS)
            {
                CPU.AccessReg(cpu);
            }
            cpu.CurrentState = CPU.StateSwitcher(cpu.CurrentState);
            
        }
        public static void fetch(CPU cpu) 
        {
            switch (cpu.RAM[cpu.R[PC]])
            {
                    ///double oprands opcodes
                case MOVMR:
                    //movMR R M xx
                case MOVRR:
                    //movMR R R xx
                case AND:
                    //movMR R M xx
                case OR:
                    cpu.op1 = cpu.RAM[cpu.R[PC] + 1];
                    cpu.op2 = cpu.RAM[cpu.R[PC] + 2];

                    break;
                
                default:
                    break;
            }    
        
        
        }

        public static void decode(CPU cpu)
        {
           
        
        }

        public static void GetFromReg(CPU cpu)
        {
        
        
        
        }


        public static void Calculate(CPU cpu)
        {
            switch (cpu.RAM[cpu.R[PC]])
            {
                ///double oprands opcodes
                case MOVMR:
                //movMR R M xx
                    cpu.R[cpu.op1] = cpu.RAM[cpu.op2];
                    break;
                case MOVRR:
                //movRR R R xx
                    cpu.R[cpu.op1] = cpu.R[cpu.op2];
                    break;
                case AND:
                //AND R R xx
                    cpu.R[cpu.op1] =cpu.R[cpu.op1] & cpu.R[cpu.op2];
                    break;
                case OR:
                //OR R R xx
                    cpu.R[cpu.op1] = cpu.R[cpu.op1]| cpu.R[cpu.op2];

                    break;

                default:
                    break;
            } 

        }

        public static void AccessMem(CPU cpu)
        {
        
        
        
        }
        public static void AccessReg(CPU cpu)
        {

            cpu.R[PC] = cpu.R[PC] + 4;

        }


    }
}
