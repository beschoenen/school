<?php namespace App\Http\Controllers\FrontEnd;

use Illuminate\Support\Facades\Mail;
use App\Http\Controllers\Controller;
use App\Models\Database\Text;
use App\Models\Database\Setting;
use Illuminate\Http\Request;

class ContactController extends Controller
{
	public function contact()
	{
		return view('front.contact', [
			'left' => Text::getText('contact', 'left'),
			'right' => Text::getText('contact', 'right')
		]);
	}

	public function contactForm()
	{
		return view('front.contactform', [
			'contact_form_enabled' => Setting::where('setting', 'contact_form_enabled')->first()
		]);
	}

	public function sendEmail(Request $request = null)
	{
		$this->validate($request, [
			'name' => 'required|string|max:75',
			'email' => 'required|email|max:75',
			'subject' => 'required|string|max:125',
			'message' => 'required|string',
			'g-recaptcha-response' => 'required|recaptcha',
		]);

		$contact_form_enabled = Setting::where('setting', 'contact_form_enabled')->first();

		if ($contact_form_enabled->value != "1") {
			return redirect()->back(); //Contact form is disabled
		}

		$contact_form_email = Setting::where('setting', 'contact_form_email')->first();

		Mail::send('emails.contactform', ['request' => $request], function ($message) use ($request, $contact_form_email) {
			$message->to($contact_form_email->value);
			$message->subject($request->subject);
			$message->from(env('MAIL_USERNAME'), 'Contactformulier');
			$message->replyTo($request->email, $request->name);
		});

		return view('front.contactform', [
			'contact_form_enabled' => $contact_form_enabled,
			'message' => 'Uw bericht is verzonden!'
		]);
	}
}