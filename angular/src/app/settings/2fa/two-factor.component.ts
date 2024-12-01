import { ToasterService } from "@abp/ng.theme.shared";
import { Component, ViewChild, ElementRef, Output, HostListener } from "@angular/core";
import { AccountsService } from "@proxy/controllers";
import * as EventEmitter from "events";

@Component({
    selector: 'two-fa-setup-modal',
    templateUrl: './two-factor.component.html',
})
export class TwoFactorSetupModalComponent {
    @ViewChild('twofactor', { static: true }) modal: ElementRef | undefined;
    @ViewChild('nameInput') nameInput: ElementRef;

    @Output() modalSave: EventEmitter = new EventEmitter();
    @Output() modalClose: EventEmitter = new EventEmitter();

    visible: boolean = false;
    loading: boolean = false;
    qrCode: string = '';
    setupCode: string = '';

    otpInput: number;
    activeStep: number = 0;
    constructor(
      private _accountService: AccountsService,
      private _toastrService: ToasterService,
    ) {}

    show(): void {
        this.visible = true;
        this.get2FASetupCode();
    }


    save(): void {

    }

    close(): void {
      this.visible = false;
      this.modalClose.emit(null);
    }

    get2FASetupCode() {
        this.loading = true;
        this._accountService.generate2Fa().subscribe(res => {
            this.loading = false;
            this.qrCode = "data:image/jpeg;base64," + res.data['img'];
            this.setupCode = res.data['text'];
        });
    }

    onActiveStepChange(step: number) {
      if (step === 2) {
        this.processAndVerifyCode(step);
      } else {
        this.activeStep = step;
      }
    }

    processAndVerifyCode(step) {
      let prevStep = this.activeStep;
      if (this.otpInput.toString().length !== 6) return;
      this._accountService.verify2FAByOtp(this.otpInput.toString()).subscribe(res => {
        if (res.success) {
          this.activeStep = step;
          this._toastrService.success(res.message);
        } else {
          this.activeStep = prevStep;
          this._toastrService.error(res.message);
        }
      });
    }

    @HostListener('document:keydown', ['$event'])
    onKeydownHandler(event: KeyboardEvent) {
        if (event.key === "Escape") {
            this.close();
        }
    }
}
