module.exports  = {
    name:'handler',
    description:'',
    run:run
}

/*
    Complete toolbox documentation:
    https://infinitered.github.io/gluegun/#/toolbox-api
*/

function run(toolbox){
    
    if(typeof toolbox.parameters.first !== "string"){
        toolbox.print.error('First parameter not entered!')
        return;
    }

    const name = toolbox.parameters.first;

    toolbox.gera.generate({
        template: 'core:ihandler.ejs',
        target: `src/Team.Accounts.Core/Handlers/I${name}Handler.cs`,
        props: { 
            name:name,
        }
    })

    toolbox.gera.generate({
        template: 'handler.ejs',
        target: `src/Team.Accounts.Application/Handlers/${name}Handler.cs`,
        props: { 
            name:name,
        }
    })

    const addTransient = `            builder.Services.AddTransient<I${name}Handler,${name}Handler>();`;

    toolbox.patching.patch('src/Team.Accounts.Api/Configurations/DependenceConfiguration.cs', { 
        insert: `\n${addTransient}`, 
        after: 'GERA-COMMANDS-ADD-REPOSITORY' 
    })

}

/*
    TOOLBOX PARAMETERS - toolbox.parameters.[name_field] :
        name	    type	    purpose	                                from the example above
        -------------------------------------------------------------------------------------------------
        plugin	    string	    the plugin used	                        'reactotron'
        command	    string	    the command used	                    'plugin'
        string	    string	    the command arguments as a string	    'MyAwesomePlugin full'
        array	    array	    the command arguments as an array	    ['MyAwesomePlugin', 'full']
        first	    string	    the 1st argument	                    'MyAwesomePlugin'
        second	    string	    the 2nd argument	                    'full'
        third	    string	    the 3rd argument	                    undefined
        options	    object	    command line options	                {comments: true, lint: 'standard'}
        argv	    object	    raw                                     argv	

*/

/*
    TOOLBOX TEMPLATE GENERATE - toolbox.template.generate({...}) 
        option	    type	    purpose	                                notes
        -----------------------------------------------------------------------------------------------------------
        template	string	    path to the EJS template	            relative from plugin's templates directory
        target	    string	    path to create the file	                relative from user's working directory
        props	    object	    more data to render in your template	
        directory	string	    where to find templates	                an absolute path (optional)
*/