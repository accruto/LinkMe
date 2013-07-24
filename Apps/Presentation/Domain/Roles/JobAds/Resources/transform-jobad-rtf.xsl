<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:idealjob="linkme:idealjob" xmlns:endorsements="linkme:endorsements">
	<xsl:output method="html" media-type="text/rtf"/>
	<xsl:param name="title"/>
	<xsl:param name="location"/>
  <xsl:param name="jobtype"/>
  <xsl:param name="salary"/>
  <xsl:param name="industry"/>
  <xsl:param name="company"/>
	<xsl:param name="content"/>
  <xsl:param name="referencenumber"/>
  <xsl:param name="bulletpoint1"/>
  <xsl:param name="bulletpoint2"/>
  <xsl:param name="bulletpoint3"/>
  <xsl:param name="headerLogo"/>
  <xsl:param name="shortLine"/>
	<xsl:param name="longLine"/>
	<xsl:param name="updatedDate"/>
	<xsl:param name="dateTimeNow"/>
  
  <xsl:template match="JobAdDoc/jobad">
		<xsl:text>{\rtf1</xsl:text>
		<xsl:text>{\fonttbl{\f0\froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f1\fswiss\fcharset0\fprq2{\*\panose 020b0604020202020204}Arial;}{\f36\fswiss\fcharset0\fprq2{\*\panose 020b0503020102020204}Franklin Gothic Book;}{\f37\froman\fcharset238\fprq2 Times New Roman CE;}{\f38\froman\fcharset204\fprq2 Times New Roman Cyr;}{\f40\froman\fcharset161\fprq2 Verdana;}{\f41\froman\fcharset162\fprq2 Times New Roman Tur;}{\f42\froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\f43\froman\fcharset178\fprq2 Times New Roman (Arabic);}{\f44\froman\fcharset186\fprq2 Times New Roman Baltic;}{\f45\froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\f47\fswiss\fcharset238\fprq2 Arial CE;}{\f48\fswiss\fcharset204\fprq2 Arial Cyr;}{\f50\fswiss\fcharset161\fprq2 Arial Greek;}{\f51\fswiss\fcharset162\fprq2 Arial Tur;}{\f52\fswiss\fcharset177\fprq2 Arial (Hebrew);}{\f53\fswiss\fcharset178\fprq2 Arial (Arabic);}{\f54\fswiss\fcharset186\fprq2 Arial Baltic;}{\f55\fswiss\fcharset163\fprq2 Arial (Vietnamese);}{\f397\fswiss\fcharset238\fprq2 Franklin Gothic Book CE;}{\f398\fswiss\fcharset204\fprq2 Franklin Gothic Book Cyr;}{\f400\fswiss\fcharset161\fprq2 Franklin Gothic Book Greek;}{\f401\fswiss\fcharset162\fprq2 Franklin Gothic Book Tur;}{\f404\fswiss\fcharset186\fprq2 Franklin Gothic Book Baltic;}}</xsl:text>
    <xsl:text>{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;\red102\green0\blue51;\red51\green51\blue51;\red153\green153\blue153;}</xsl:text>
    <xsl:text>{\stylesheet{\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \snext0 Normal;}{\*\cs10 \additive \ssemihidden Default Paragraph Font;}{\*\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\trcbpat1\trcfpat1\tscellwidthfts0\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv \ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs20\lang1024\langfe1024\cgrid\langnp1024\langfenp1024 \snext11 \ssemihidden Normal Table;}{\s15\ql \li0\ri0\widctlpar\tqc\tx4320\tqr\tx8640\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext15 \styrsid3700768 header;}{\s16\ql \li0\ri0\widctlpar\tqc\tx4320\tqr\tx8640\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 \sbasedon0 \snext16 \styrsid3700768 footer;}{\*\cs17 \additive \ul\cf2 \sbasedon10 \styrsid3700768 Hyperlink;}}</xsl:text>
    <xsl:text>{\*\latentstyles\lsdstimax156\lsdlockeddef0}</xsl:text>
    <xsl:text>{\*\pgptbl {\pgp\ipgp4\itap0\li0\ri0\sb0\sa0}{\pgp\ipgp4\itap0\li0\ri0\sb0\sa0}{\pgp\ipgp4\itap0\li0\ri0\sb0\sa0}{\pgp\ipgp0\itap0\li0\ri0\sb0\sa0}}</xsl:text>
    <xsl:text>{\*\rsidtbl \rsid5394\rsid3700768\rsid4026808\rsid5064455\rsid6488721\rsid7361170\rsid7668375\rsid7810014\rsid11827082\rsid15624247}</xsl:text>
    <xsl:text>{\header \pard\plain \s15\ql \li0\ri0\widctlpar\tqr\tx8640\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid68203 \f1\fs20\lang1033\langfe1033\cgrid\langnp1033\langfenp1033 Downloaded </xsl:text>
		<xsl:value-of disable-output-escaping="yes" select="$dateTimeNow"/>
    <xsl:text>{\insrsid68203\tab</xsl:text>
    <xsl:value-of disable-output-escaping="yes" select="$headerLogo"/>
		<xsl:text>}{\insrsid13722960 \par }}</xsl:text>
		<xsl:text>{\headerf}</xsl:text>

    <xsl:variable name="StringIn">
      <xsl:text>{\b\f40\fs32\cf17\insrsid6118751\charrsid6118751 Job}</xsl:text>
      <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid3700768 \par }</xsl:text>
      
      <!-- Title -->
      <xsl:text>{\b\f36\fs32\cf17\insrsid3700768\charrsid11827082 </xsl:text>
      <xsl:value-of disable-output-escaping="yes" select="title"/>
      <xsl:text>\par }</xsl:text>

      <!-- Location -->
      <xsl:if test="location">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768 \par }{\b\f36\fs20\cf1\insrsid11827082 </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="location" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>

      <!-- Salary -->
      <xsl:if test="salary">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768 \par }{\b\f36\fs20\cf1\insrsid11827082 </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="salary" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>

      <!-- Industry -->
      <xsl:if test="industry">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid5064455 </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="industry" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>

      <!-- reference -->
      <xsl:if test="referencenumber">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid5064455 Reference no. </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="referencenumber" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>

      <!-- company -->
      <xsl:if test="company">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid5064455 Company name </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="company" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>

      <!-- bullet points -->
      <xsl:if test="bulletpoint1">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid5064455\bullet </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="bulletpoint1" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>
      <xsl:if test="bulletpoint2">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid5064455\bullet </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="bulletpoint2" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>
      <xsl:if test="bulletpoint3">
        <xsl:text>{\f36\fs20\cf1\insrsid3700768\charrsid5064455\bullet </xsl:text>
        <xsl:value-of disable-output-escaping="yes" select="bulletpoint3" />
        <xsl:text>\par }</xsl:text>
      </xsl:if>

      <!-- content -->
			<xsl:if test="content">
			<xsl:text>\par </xsl:text>
        
        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Description \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->
        
				<xsl:text>\f1\fs20\sb120 </xsl:text>
				<xsl:value-of disable-output-escaping="yes" select="content"/>
				<xsl:text>\par \par </xsl:text>
			</xsl:if>

			<!-- Work Experience -->
			<xsl:if test="experience/job">
        
        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Employment History \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->
        
        <xsl:text>\pard \ql </xsl:text>
				<xsl:apply-templates select="//job"/>
			</xsl:if>
			
			<!-- skills -->
      <!-- <xsl:text>\page </xsl:text-->
			<xsl:if test="(skills and (skills != '' or (skills/cleantext and skills/cleantext != '') ))">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Skills \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

        <xsl:text>\f1\fs20\sb120 </xsl:text>
			<xsl:apply-templates select="skills"/>
			<xsl:text>\par\par </xsl:text>
			</xsl:if>
			
			<!-- education -->
			<xsl:if test="education/school">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Education \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

        <xsl:text>\pard \ql </xsl:text>
			<xsl:apply-templates select="education"/>
			<xsl:text>\par </xsl:text>
			</xsl:if>
			
			<!-- courses -->
			<xsl:if test="//courses and //courses != ''">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Courses \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

        <xsl:text>\pard \ql </xsl:text>
				<xsl:for-each select="//courses">
					<xsl:value-of disable-output-escaping="yes" select="node()"/>
					<xsl:text>\par </xsl:text>
				</xsl:for-each>
			<xsl:text>\par </xsl:text>
			</xsl:if>
			
			<!-- awards -->
			<xsl:if test="//honors and //honors != ''">

      <!-- Grey underlined heading -->
      <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
      <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Awards \par }</xsl:text>
      <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
      <!-- End heading -->

			<xsl:text>\pard \ql </xsl:text>
				<xsl:for-each select="//honors">
					<xsl:value-of disable-output-escaping="yes" select="node()"/>
					<xsl:text>\par </xsl:text>
				</xsl:for-each>
			<xsl:text>\par </xsl:text>
			</xsl:if>
			
			<!-- Professional -->
			<xsl:if test="(professional and (professional != '' or (professional/cleantext and professional/cleantext != '') ))">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Professional \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

        <xsl:text>\f1\fs20\sb120 </xsl:text>				
				<xsl:value-of disable-output-escaping="yes" select="professional"/>
				<xsl:text>\par \par </xsl:text>	
			</xsl:if>
			
			<!-- Interests -->
			<xsl:if test="(statements/activities and (statements/activities != '' or (statements/activities/cleantext and statements/activities/cleantext != '') ))">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Interests \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

        <xsl:text>\f1\fs20\sb120 </xsl:text>				
				<xsl:value-of disable-output-escaping="yes" select="statements/activities"/>
				<xsl:text>\par \par </xsl:text>	
			</xsl:if>
			
			<!-- Citizenship -->
			<xsl:if test="(statements/citizenship and (statements/citizenship != '' or (statements/citizenship/cleantext and statements/citizenship/cleantext != '') ))">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Citizenship \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

        <xsl:text>\f1\fs20\sb120 </xsl:text>				
				<xsl:value-of disable-output-escaping="yes" select="statements/citizenship"/>
				<xsl:text>\par \par </xsl:text>	
			</xsl:if>	
			
			
			<!-- Affiliations -->
			<xsl:if test="(statements/affiliations and (statements/affiliations != '' or (statements/affiliations/cleantext and statements/affiliations/cleantext != '') ))">
				<xsl:apply-templates select="statements/affiliations"/>
				<xsl:text>\par </xsl:text>
			</xsl:if>
			
			<!-- Other -->
			<xsl:if test="(other and (other != '' or (other/cleantext and other/cleantext != '') ))">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Other Information \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

				<xsl:text>\par \f1\fs20\sb120 </xsl:text>				
				<xsl:value-of disable-output-escaping="yes" select="other"/>
				<xsl:text>\par \par </xsl:text>	
			</xsl:if>
			
			<!-- Referees -->
			<xsl:if test="(statements/references and (statements/references != ''
			 or (statements/references/cleantext and statements/references/cleantext != '') ))">

        <!-- Grey underlined heading -->
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Referees \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>
        <!-- End heading -->

				<xsl:text>\par \f1\fs20\sb120 </xsl:text>				
				<xsl:value-of disable-output-escaping="yes" select="statements/references"/>
				<xsl:text>\par \par </xsl:text>	
			</xsl:if>
				
			<!-- TODO32: References -->
			<!-- Grey underlined heading -->
      <!--
			<xsl:if test="endorsements:HasNext()">
        
        <xsl:text>\pard \ql \li0\ri0\sb120\sa120\sl288\slmult1\widctlpar\brdrb\brdrs\brdrw5\brsp20\brdrcf16 </xsl:text>
        <xsl:text>\faauto\rin0\lin0\itap0\pararsid4026808 {\b\f36\fs26\cf17\insrsid4026808 Endorsements \par }</xsl:text>
        <xsl:text>\pard \ql \li0\ri0\sl287\slmult1\widctlpar\faauto\rin0\lin0\itap0\pararsid3700768 </xsl:text>

        <xsl:text>\pard \ql </xsl:text>
					<xsl:call-template name="endorsements"/>
					<xsl:text>\par </xsl:text>	
			</xsl:if>
      -->
			<xsl:text>}</xsl:text>
		
		</xsl:variable>
		<xsl:call-template name="lf2br">
			<xsl:with-param name="StringToTransform" select="$StringIn"/>
		</xsl:call-template>
	</xsl:template>	
	
	<xsl:template name="lf2br">
		<!-- import $StringToTransform -->
		<xsl:param name="StringToTransform"/>
		<xsl:choose>
			<!-- string contains linefeed -->
			<xsl:when test="contains($StringToTransform,'&#xA;')">
				<!-- output substring that comes before the first linefeed -->
				<!-- note: use of substring-before() function means        -->
				<!-- $StringToTransform will be treated as a string,       -->
				<!-- even if it is a node-set or result tree fragment.     -->
				<!-- So hopefully $StringToTransform is really a string!   -->
				<xsl:value-of disable-output-escaping="yes" select="substring-before($StringToTransform,'&#xA;')"/>
				<!-- by putting a 'br' element in the result tree instead  -->
				<!-- of the linefeed character, a <br> will be output at   -->
			       <!-- that point in the HTML                                -->

				 <xsl:text>\par </xsl:text>
				<!-- repeat for the remainder of the original string -->
				<xsl:call-template name="lf2br">
					<xsl:with-param name="StringToTransform">
						<xsl:value-of disable-output-escaping="yes" select="substring-after($StringToTransform,'&#xA;')"/>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:when>
			<!-- string does not contain newline, so just output it -->
			<xsl:otherwise>
				<xsl:value-of disable-output-escaping="yes" select="$StringToTransform"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

  <xsl:template match="JobAdDoc/special">
    <!-- Ignore the <special> element. -->
  </xsl:template>

  <xsl:template name="shortDate">
    <xsl:param name="date" />
    <xsl:param name="isEnd" />
    <xsl:choose>
      <xsl:when test="contains($date, 'Current')">
        <xsl:value-of disable-output-escaping="yes" select="$date" />
      </xsl:when>
      <xsl:when test="contains($date, ' ')">
        <xsl:variable name="firstPart" select="substring-before($date, ' ')" />
        <xsl:variable name="endPart" select="substring-after($date, ' ')" />
        <!-- Abbreviate long dates into short, e.g. December -> Dec -->
        <xsl:choose>
          <xsl:when test="$firstPart = 'January'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Jan</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'February'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Feb</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'March'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Mar</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'April'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Apr</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'May'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>May</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'June'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Jun</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'July'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Jul</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'August'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Aug</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'September'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Sep</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'October'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Oct</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'November'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Nov</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
          <xsl:when test="$firstPart = 'December'">
            <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
            <xsl:text>Dec</xsl:text>
            <xsl:text> </xsl:text>
            <xsl:value-of disable-output-escaping="yes" select="$endPart" />
          </xsl:when>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of disable-output-escaping="yes" select="$isEnd" />
        <xsl:value-of select="$date" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
 
</xsl:stylesheet>
