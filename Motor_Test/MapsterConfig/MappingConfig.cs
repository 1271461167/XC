using Mapster;
using Motor_Test.Dto;
using Motor_Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor_Test.MapsterConfig
{
    public class MappingConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<JogDto, JogModel>.NewConfig()
                .Map(dest => dest.Vel, src => src.Vel * src.Pul / 1000)
                .Map(dest => dest.Acc, src => src.Vel * src.Pul / 1000 / src.Acc)
                .Map(dest => dest.Dec, src => src.Vel * src.Pul / 1000 / src.Dec);
            TypeAdapterConfig<TrapDto, TrapModel>.NewConfig()
                .Map(dest => dest.Vel, src => src.Vel * src.Pul / 1000)
                .Map(dest => dest.Acc, src => src.Vel * src.Pul / 1000 / src.Acc)
                .Map(dest => dest.Dec, src => src.Vel * src.Pul / 1000 / src.Dec)
                .Map(dest => dest.Position, src => src.Position * src.Pul);
        }
        public static void JogModelConfigure(bool b)
        {
            if (b)
            {
             TypeAdapterConfig<JogDto, JogModel>.NewConfig()
                .Map(dest => dest.Vel, src => src.Vel * src.Pul / 1000)
                .Map(dest => dest.Acc, src => src.Vel * src.Pul / 1000 / src.Acc)
                .Map(dest => dest.Dec, src => src.Vel * src.Pul / 1000 / src.Dec);
            }
            else
            {
                TypeAdapterConfig<JogDto, JogModel>.NewConfig()
                .Map(dest => dest.Vel, src => -src.Vel * src.Pul / 1000)
                .Map(dest => dest.Acc, src => src.Vel * src.Pul / 1000 / src.Acc)
                .Map(dest => dest.Dec, src => src.Vel * src.Pul / 1000 / src.Dec);
            }
        }
        public static void ArrayModelConfigure(int index)
        {
            switch (index)
            {
                case 1:
                    TypeAdapterConfig<ArrayModelDto, ArrayModel>.NewConfig()
                .Map(dest => dest.Vel, src => src.Vel * src.Pul1 / 1000)
                .Map(dest => dest.Acc, src => src.Vel * src.Pul1 / 1000 / src.Acc)
                .Map(dest => dest.Dec, src => src.Vel * src.Pul1 / 1000 / src.Dec);
                    break;
                    case 2:
                    TypeAdapterConfig<ArrayModelDto, ArrayModel>.NewConfig()
                .Map(dest => dest.Vel, src => src.Vel * src.Pul2 / 1000)
                .Map(dest => dest.Acc, src => src.Vel * src.Pul2 / 1000 / src.Acc)
                .Map(dest => dest.Dec, src => src.Vel * src.Pul2 / 1000 / src.Dec);
                    break;
                    default: break;
            }

        }
    }
}
