<?php

use Illuminate\Foundation\Testing\DatabaseMigrations;
use Illuminate\Foundation\Testing\DatabaseTransactions;

class ContactFormTest extends TestCase
{
    use DatabaseTransactions;
    
    public function testFrontEnd()
    {
        //Test if mail server works
        try{
            $transport = Swift_SmtpTransport::newInstance(env('MAIL_HOST'), env('MAIL_PORT'), env('MAIL_ENCRYPTION'));
            $transport->setUsername(env('MAIL_USERNAME'));
            $transport->setPassword(env('MAIL_PASSWORD'));
            $mailer = \Swift_Mailer::newInstance($transport);
            $mailer->getTransport()->start();
        } catch (Swift_TransportException $e) {
            return $e->getMessage();
        } catch (Exception $e) {
            return $e->getMessage();
        }
    }
}