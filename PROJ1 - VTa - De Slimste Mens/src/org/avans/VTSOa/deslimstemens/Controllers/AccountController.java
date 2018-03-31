package org.avans.VTSOa.deslimstemens.Controllers;

import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Account;
import org.avans.VTSOa.deslimstemens.Repositories.AccountRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;
import org.avans.VTSOa.deslimstemens.Views.AccountView;
import org.omg.SendingContext.RunTime;

import java.sql.SQLException;

public class AccountController extends SceneController {

	private AccountView view;

	public AccountController() {
		this.view = new AccountView(this);
	}

	public void checkUpdatePassword(String passNew, String passOld){
		boolean isSame = RunTimeSettings.user.getPassword().equals(passNew);
		if(!RunTimeSettings.user.getPassword().equals(passOld) && !isSame){
			Account newAccount = RunTimeSettings.user;
			newAccount.setPassword(passNew);
			try {
				AccountRepository.update(newAccount);
			} catch (SQLException e) {
				e.printStackTrace();
			}
			RunTimeSettings.user = newAccount;
			showErrorMessage("Wachtwoord gewijzigd");
			this.view.clearText(true);
		}
		else if(!isSame){
			showErrorMessage("Wachtwoord is niet nieuw");
			this.view.clearText(true);
		}
		else{
			showErrorMessage("Oud wachtwoord komt niet overheen!");
			this.view.clearText(false);
		}
	}

	public void showErrorMessage(String msg) {
		this.view.infoText.setText(msg);
	}

	@Override
	public SceneView getView() {
		return view;
	}

	@Override
	public String getName() {
		return "accountpage";
	}
}
