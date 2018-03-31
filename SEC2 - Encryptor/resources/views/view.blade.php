<html>
    <head>
        <title>Encrypt your message</title>
        <style>
            input, textarea {
                width: 100%;
            }

            textarea {
                height: 300px;
            }

            body {
                width: 500px;
            }
        </style>
    </head>
    <body>
        @isset($error)
            <div>
                <span>{{ $error }}</span>
                <br/>
            </div>
        @endisset
        <form method="post" action="/">
            {{ csrf_field() }}
            <span>Naam: </span>
            <br/>
            <input type="text" name="name" value="{{ $name ?? '' }}"/>
            <br/>
            <span>Bericht:</span>
            <br/>
            <textarea name="message">{{ $message ?? '' }}</textarea>
            <br/>
            <span>Wachtwoord:</span>
            <br/>
            <input type="password" name="password"/>
            <br/>
            <input type="submit">
        </form>
    </body>
</html>