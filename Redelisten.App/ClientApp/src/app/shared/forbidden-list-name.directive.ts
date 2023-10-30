import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator, ValidatorFn } from '@angular/forms';

/** A lists' name can't match the given regular expression */
export function forbiddenListNameValidator(nameRe: RegExp): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const forbidden = nameRe.test(control.value);
    return forbidden ? {forbiddenListName: {value: control.value}} : null;
  };
}

@Directive({
  selector: '[appForbiddenListName]',
  providers: [{provide: NG_VALIDATORS, useExisting: ForbiddenValidatorDirective, multi: true}]
})
export class ForbiddenValidatorDirective implements Validator {
  @Input('appForbiddenListName') forbiddenListName = '';

  validate(control: AbstractControl): ValidationErrors | null {
    return this.forbiddenListName ? forbiddenListNameValidator(new RegExp(this.forbiddenListName, 'i'))(control)
                              : null;
  }
}
