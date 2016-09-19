module.exports = function (context, myTimer) {
    var timeStamp = new Date().toISOString();
    
    context.log(`putona! ${JSON.stringify(context)}`);

    if(myTimer.isPastDue)
    {
        context.log('Node.js is running late!');
    }
    context.log('Node.js timer trigger function ran!', timeStamp);   
    
    context.done();
};