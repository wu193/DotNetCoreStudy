using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelValidatorSamples.Validators
{
    public class CustomLanguageManager: LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("zh-CN", "LengthValidator", "{PropertyName}的长度必须在{MinLength}-{MaxLength}字符");
        }
    }
}
